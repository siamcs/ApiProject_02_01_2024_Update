using ApiProject_02_01_2024.DTOs;

namespace ApiProject_02_01_2024.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<List<CustomerVM>> GetAllAsync();
        Task<CustomerVM> GetByIdAsync(int id);
        Task<bool> SaveAsync(CustomerVM customerVM);
        Task<bool> UpdateAsync(CustomerVM customerVM);
        Task<bool> DeleteAsync(int id);
        Task<string> GenerateNextCustomerCode();
        Task<string> GenerateNextCustomerDeliveryAddressCode();
        Task<List<CommonSelectModelVM>> DropDown();
        string GetLocalIP();
        string GetMacAddress();
        Task<bool> IsExistAsync(string name, int code);
    }
}
