using ApiProject_02_01_2024.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiProject_02_01_2024.DTOs
{
    public class CustomerVM
    {
        public int Id { get; set; }


        public string CustomerCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter customer name!")]
        public string CustomerName { get; set; } = string.Empty;
        public string CusTypeCode {  get; set; } = string.Empty;
        public string CustomerTypeName { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        public  string? CheckBox { get; set; }
        public IList<CustomerDeliveryAddressVM>? CustomerDeliveryAddresses { get; set; } = new List<CustomerDeliveryAddressVM>();
    }
}
