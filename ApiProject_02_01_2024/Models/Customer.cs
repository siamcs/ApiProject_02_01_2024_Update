using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ApiProject_02_01_2024.Models
{
    [Table("Customers")]
    [Index(nameof(CustomerCode), IsUnique = true)]
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Customer code cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(8)")]
        public string? CustomerCode { get; set; } = string.Empty;


        [Required, DisplayName("Customer Name")]
        [StringLength(100, ErrorMessage = "Customer Name cannot be longer than 100 characters")]
        public string CustomerName { get; set; } = string.Empty;


        //Entry last Date
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? LDate { get; set; } = DateTime.Now;


        //Computer last Internet Protocol address
        [Required]
        [StringLength(50, ErrorMessage = "Last IP address cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string? LIP { get; set; } = string.Empty;


        //Computer last MAC address
        [Required]
        [StringLength(50, ErrorMessage = "Last MAC address cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        public string? LMAC { get; set; } = string.Empty;


        //Last modify date
        [Required, DisplayName("Modify Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public IList<CustomerDeliveryAddress>? CustomerDeliveryAddresses { get; set; }

        [AllowNull]
        public string? CusTypeCode { get; set; } = string.Empty;
        public CustomerType? CustomerType { get; set; }
        public bool? IsActive { get; set; }

        public string? CheckBox { get; set; }
    }
}
