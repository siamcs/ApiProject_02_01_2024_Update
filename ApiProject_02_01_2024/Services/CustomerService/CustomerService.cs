using ApiProject_02_01_2024.DTOs;
using ApiProject_02_01_2024.Models;
using ApiProject_02_01_2024.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace ApiProject_02_01_2024.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer, int> _customerRepository;
        private readonly IGenericRepository<CustomerType,int> custoType;
        private readonly IGenericRepository<CustomerDeliveryAddress, int> _customerDeliveryRepository;
      
        private readonly IWebHostEnvironment _web;

        public CustomerService(IGenericRepository<Customer, int> customerRepository, IGenericRepository<CustomerDeliveryAddress, int> customerDeliveryRepository, IWebHostEnvironment web, IGenericRepository<CustomerType, int> custoType)
        {
            _customerRepository = customerRepository;
            _customerDeliveryRepository = customerDeliveryRepository;
            _web = web;
            this.custoType = custoType;
        }

        public async Task<List<CommonSelectModelVM>> DropDown()
        {
            var result=  await custoType.All().Select(x => new CommonSelectModelVM
            {
                Code=x.CusTypeCode,
                Name=x.CustomerTypeName
            }).ToListAsync();
            return result;
        }


        #region  GetAllData

        public async Task<List<CustomerVM>> GetAllAsync()
        {
            return await _customerRepository.All()
                .Include(x => x.CustomerDeliveryAddresses)
                .Select(cs => new CustomerVM
                {
                    Id = cs.Id,
                    CustomerCode = cs.CustomerCode,
                    CustomerName = cs.CustomerName,
                    CusTypeCode = cs.CusTypeCode,
                    IsActive = cs.IsActive,
                    CheckBox = cs.CheckBox,

                    CustomerTypeName = cs.CustomerType.CustomerTypeName,
                    CustomerDeliveryAddresses = cs.CustomerDeliveryAddresses.Select(cusdelAddress => new CustomerDeliveryAddressVM
                    {
                        Id = cusdelAddress.Id,
                        CusDelAddCode = cusdelAddress.CusDelAddCode,
                        CustomerCode = cusdelAddress.CustomerCode,
                        ContactPerson = cusdelAddress.ContactPerson,
                        CPPhoneNo = cusdelAddress.CPPhoneNo,
                        CPAddress = cusdelAddress.CPAddress

                    }).ToList()
                }).ToListAsync();
        }




        #endregion

        #region GetByIdData
        public async Task<CustomerVM> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.All().Include(x=>x.CustomerType)
                .Include(x => x.CustomerDeliveryAddresses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return null;
            }

            return new CustomerVM
            {
                Id = customer.Id,
                CustomerCode = customer.CustomerCode,
                CustomerName = customer.CustomerName,
                IsActive=customer.IsActive,
                CheckBox=customer.CheckBox,
                CusTypeCode=customer.CusTypeCode,
                CustomerTypeName = customer.CustomerType.CustomerTypeName,
                CustomerDeliveryAddresses = customer.CustomerDeliveryAddresses.Select(cusDelAddress => new CustomerDeliveryAddressVM
                {
                    Id = cusDelAddress.Id,
                    CusDelAddCode = cusDelAddress.CusDelAddCode,
                    CustomerCode = cusDelAddress.CustomerCode,
                    ContactPerson = cusDelAddress.ContactPerson,
                    CPPhoneNo = cusDelAddress.CPPhoneNo,
                    CPAddress = cusDelAddress.CPAddress
                }).ToList()
            };
        }
        #endregion



        private string GenerateNextCusDelAddCode(ref int nextCode)
        {
            return nextCode.ToString("D4");
        }

       

        public async Task<bool> SaveAsync(CustomerVM customerVM)
        {

            try
            {
                Customer customer = new Customer();
                customer.CustomerCode = await GenerateNextCustomerCode();
                customer.CustomerName = customerVM.CustomerName;
                customer.CusTypeCode = customerVM.CusTypeCode ?? string.Empty;
                customer.IsActive = customerVM.IsActive ?? true;
                customer.CheckBox =string.Join(",", customerVM.CheckBox) ?? string.Empty;
                customer.LDate = DateTime.Now;
                customer.LIP = GetLocalIP();
                customer.LMAC = GetMacAddress();
                await _customerRepository.AddAsync(customer);
                // Save customer delivery addresses
                if (customerVM.CustomerDeliveryAddresses != null && customerVM.CustomerDeliveryAddresses.Any())
                {
                    var lastCode = _customerDeliveryRepository.All().Max(x => x.CusDelAddCode);
                    int nextCusDelAddCode = 1;
                    if (!string.IsNullOrEmpty(lastCode))
                    {
                        int lastNumber = int.Parse(lastCode.TrimStart('0'));
                        nextCusDelAddCode = lastNumber + 1;
                    }

                    foreach (var address in customerVM.CustomerDeliveryAddresses)
                    {
                        var oCusDelAddress = new CustomerDeliveryAddress
                        {
                            CustomerCode = customer.CustomerCode,
                            ContactPerson = address.ContactPerson,
                            CusDelAddCode = GenerateNextCusDelAddCode(ref nextCusDelAddCode),
                            CPPhoneNo = address.CPPhoneNo,
                            CPAddress = address.CPAddress,

                        };
                        await _customerDeliveryRepository.AddAsync(oCusDelAddress);
                        nextCusDelAddCode++;
                    }
                };





                return true;
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }


        //

        public async Task<bool> UpdateAsync(CustomerVM customerVM)
        {
            //


            //
            var customer = await _customerRepository.GetByIdAsync(customerVM.Id);
            if (customer == null)
            {
                return false;
            }
            customer.CustomerCode = customerVM.CustomerCode;
            customer.CustomerName = customerVM.CustomerName;
            customer.CusTypeCode=customerVM.CusTypeCode ?? string.Empty;
            customer.ModifyDate = DateTime.Now;
            customer.IsActive = customerVM.IsActive ?? true;
            customer.CheckBox = string.Join(",", customerVM.CheckBox) ?? string.Empty;
            customer.LIP = GetLocalIP();
            customer.LMAC = GetMacAddress();
            await _customerRepository.UpdateAsync(customer);

            // Remove existing delivery addresses
            var existingAddresses = _customerDeliveryRepository.All().Where(x => x.CustomerCode == customer.CustomerCode).ToList();
            await _customerDeliveryRepository.DeleteRangeAsync(existingAddresses);

            // Save customer delivery addresses
            if (customerVM.CustomerDeliveryAddresses != null && customerVM.CustomerDeliveryAddresses.Any())
            {
                var lastCode = _customerDeliveryRepository.All().Max(x => x.CusDelAddCode);
                int nextCusDelAddCode = 1;
                if (!string.IsNullOrEmpty(lastCode))
                {
                    int lastNumber = int.Parse(lastCode.TrimStart('0'));
                    nextCusDelAddCode = lastNumber + 1;
                }

                foreach (var address in customerVM.CustomerDeliveryAddresses)
                {
                    var oCusDelAddress = new CustomerDeliveryAddress
                    {
                        CustomerCode = customer.CustomerCode,
                        ContactPerson = address.ContactPerson,
                        CusDelAddCode = GenerateNextCusDelAddCode(ref nextCusDelAddCode), 
                        CPPhoneNo = address.CPPhoneNo,  
                        CPAddress = address.CPAddress,
                   
                    };

                    await _customerDeliveryRepository.UpdateAsync(oCusDelAddress);  
                    nextCusDelAddCode++;
                }
            }

            return true;
        }



       

        #region SingleDeletemethdod 
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            
            if (customer == null)
            {
                return false;
            }
            await _customerRepository.DeleteAsync(customer);
            return true;
        }
        #endregion

        #region Next CustomerCode
        public async Task<string> GenerateNextCustomerCode()
        {
            var customers = await _customerRepository.GetAllAsync();
            var lastCode = customers.Max(b => b.CustomerCode);
            string nextCode = "CUS0001";
            if (!string.IsNullOrEmpty(lastCode))
            {
                int lastNumber = int.Parse(lastCode.Substring(3));
                lastNumber++;
                nextCode = $"CUS{lastNumber:D4}";
            }
            return nextCode;
        }

       

        public Task<string> GenerateNextCustomerDeliveryAddressCode()
        {
            var lastCode = _customerDeliveryRepository.All().Max(x => x.CusDelAddCode);
            int nextCode = 1;
            if (!string.IsNullOrEmpty(lastCode))
            {
                int lastNumber = int.Parse(lastCode.TrimStart('0'));
                lastNumber++;
                nextCode = lastNumber;
            }
            return Task.FromResult(nextCode.ToString("D4")); // R
        }
        #endregion

        #region IP and MacAddress
        public string GetLocalIP()
        {
            string ipAddress = string.Empty;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);

            foreach (var networkInterface in networkInterfaces)
            {
                var properties = networkInterface.GetIPProperties();
                var ipv4Address = properties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                if (ipv4Address != null)
                {
                    ipAddress = ipv4Address.Address.ToString();
                    break;
                }
            }

            return ipAddress;
        }


        public string GetMacAddress()
        {
            string macAddress = string.Empty;
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
           .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback);
            foreach (var networkInterface in networkInterfaces)
            {
                macAddress = networkInterface.GetPhysicalAddress().ToString();
                if (!string.IsNullOrEmpty(macAddress))
                {
                    break;
                }
            }

            return macAddress;
        }

        public async Task<bool> IsExistAsync(string name, int code)
        {
            return await _customerRepository.All().AnyAsync(x => x.CustomerName == name && x.Id != code);
        }

       

        #endregion


    }
}
