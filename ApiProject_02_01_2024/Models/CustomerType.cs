using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiProject_02_01_2024.Models
{
    [Table("CustomerTypes")]
    [Index(nameof(CusTypeCode), IsUnique = true)]
    [Index(nameof(CustomerTypeName), IsUnique = true)]
    public class CustomerType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Customer type code is required"), DisplayName("Customer Type Code")]
        [StringLength(50, ErrorMessage = "Customer type code cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string? CusTypeCode { get; set; } = string.Empty;


        [Required(ErrorMessage = "Customer type is required")]
        [StringLength(50, ErrorMessage = "Customer Id cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerTypeName { get; set; } = string.Empty;
        public IList<Customer>? Customers { get; set; }
    }
}
