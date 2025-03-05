using ApiProject_02_01_2024.DTOs;

namespace ApiProject_02_01_2024.Services.CustomerTypeService
{
    public interface ICustomerTypeService
    {

        Task<List<CustomerTypeVM>> GetAllAsync();
        Task<CustomerTypeVM> GetByIdAsync(int id);
        Task<bool> SaveAsync(CustomerTypeVM  customerTypeVM);
        Task<bool> UpdateAsync(CustomerTypeVM  customerTypeVM);
        Task<bool> DeleteAsync(int id);
        Task<string> GenerateNextCusTypeCodeAsync();
      Task<IEnumerable<CommonSelectModelVM>> DropSelection();
        Task<bool> IsExistAsync(string name, string code);
    }
}
