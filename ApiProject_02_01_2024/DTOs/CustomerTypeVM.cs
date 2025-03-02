using ApiProject_02_01_2024.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiProject_02_01_2024.DTOs
{
    public class CustomerTypeVM
    {
       
        public int Id { get; set; }
        [StringLength(50)]
        public string? CusTypeCode { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string? CustomerTypeName { get; set; } = string.Empty;
        public IList<Customer>? Customers { get; set; }
    }
}
