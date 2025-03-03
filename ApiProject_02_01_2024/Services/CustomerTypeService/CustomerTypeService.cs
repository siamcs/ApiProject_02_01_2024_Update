using ApiProject_02_01_2024.DTOs;
using ApiProject_02_01_2024.Models;
using ApiProject_02_01_2024.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiProject_02_01_2024.Services.CustomerTypeService
{
    public class CustomerTypeService : ICustomerTypeService
    {
        private readonly IGenericRepository<CustomerType, int> _customerType;

        public CustomerTypeService(IGenericRepository<CustomerType, int> customerType)
        {
            _customerType = customerType;
        }

       
        public async Task<List<CustomerTypeVM>> GetAllAsync()
        {
            try
            {
                var entiity = await _customerType.GetAllAsync();
                return entiity?.Select(b => new CustomerTypeVM
                {
                    Id = b.Id,
                    CustomerTypeName = b.CustomerTypeName,
                    CusTypeCode = b.CusTypeCode,

                }).ToList() ?? new List<CustomerTypeVM>();
            }
            catch (Exception ex)
            { 
            Console.WriteLine(ex.Message);
            return new List<CustomerTypeVM>();
            }
           
        }

        public async Task<CustomerTypeVM> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _customerType.GetByIdAsync(id);
                if (entity == null)
                {
                    return null;
                }
                return new CustomerTypeVM
                {
                    Id = entity.Id,
                    CustomerTypeName = entity.CustomerTypeName,
                    CusTypeCode = entity.CusTypeCode,
                };
            }
            catch (Exception ex) 
            { 
            Console.WriteLine($"{ex.Message}");
                return null;
            }
         
        }

        public async Task<bool> SaveAsync(CustomerTypeVM customerTypeVM)
        {
            await _customerType.BeginTransactionAsync();

            try
            {
                var customerType = new CustomerType
                {
                    Id = customerTypeVM.Id,
                    CusTypeCode = await GenerateNextCusTypeCodeAsync(),
                    CustomerTypeName = customerTypeVM.CustomerTypeName
                };

                await _customerType.AddAsync(customerType);
                await _customerType.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Error in SaveAsync: {ex.Message}");
                await _customerType.RollbackTransactionAsync();
                return false;
            }
        }


        public async Task<bool> UpdateAsync(CustomerTypeVM customerTypeVM)
        {
            await _customerType.BeginTransactionAsync();

            try
            {
              
                var existingCustomerType = await _customerType.GetByIdAsync(customerTypeVM.Id);
                if (existingCustomerType == null)
                {
                    return false;
                }

                existingCustomerType.CustomerTypeName = customerTypeVM.CustomerTypeName;
                existingCustomerType.CusTypeCode = await GenerateNextCusTypeCodeAsync();
                await _customerType.UpdateAsync(existingCustomerType);
                await _customerType.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"Error in UpdateAsync: {ex.Message}");
                await _customerType.RollbackTransactionAsync();
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            await _customerType.BeginTransactionAsync();
            try
            {
                var ids = await _customerType.GetByIdAsync(id);
                await _customerType.DeleteAsync(ids);
                await _customerType.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message );
                await _customerType.RollbackTransactionAsync(); 
                return false;
            }
        }

        public async Task<IEnumerable<CommonSelectModelVM>> DropSelection()
        {
            var result=await _customerType.All().Select(x=> new CommonSelectModelVM 
            {
                Code=x.CusTypeCode,
                Name=x.CustomerTypeName
            }).ToListAsync();
            return result;
        }

        public async Task<string> GenerateNextCusTypeCodeAsync()
        {
            var cusType = await _customerType.GetAllAsync();
            var lastCode = cusType.Max(b => b.CusTypeCode);
            int nextCode = 1;
            if (!string.IsNullOrEmpty(lastCode))
            {
                int lastNumber = int.Parse(lastCode.TrimStart('0'));
                lastNumber++;
                nextCode = lastNumber;
            }
            return nextCode.ToString("D2");
        }
        public async Task<bool> IsExistAsync(string name, int code)
        {
            return await _customerType.All().AnyAsync(x => x.CustomerTypeName == name && x.Id !=code);
        }
    }
}
