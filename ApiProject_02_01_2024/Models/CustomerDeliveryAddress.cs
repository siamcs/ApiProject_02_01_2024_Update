using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ApiProject_02_01_2024.Models
{
    [Table("CustomerDeliveryAddresses")]
    [Index(nameof(CusDelAddCode), IsUnique = true)]
    public class CustomerDeliveryAddress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Delivery info code cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string? CusDelAddCode { get; set; } = string.Empty;

        [Required]
        //[ForeignKey(nameof(Customer.CustomerCode))]
        public string? CustomerCode { get; set; } = string.Empty;
        public Customer? Customer { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Contact person cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string? ContactPerson { get; set; } = string.Empty;
        [Required]
        [StringLength(15, ErrorMessage = "Phone no cannot be longer than 15 characters")]
        [Column(TypeName = "nvarchar(15)")]
        public string? CPPhoneNo { get; set; } = string.Empty;
        [Required(ErrorMessage = "Delivery address is required"), DisplayName("Delivery Address")]
        public string? CPAddress { get; set; } = string.Empty;

    }
}
