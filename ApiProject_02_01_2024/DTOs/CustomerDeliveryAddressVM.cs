using ApiProject_02_01_2024.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ApiProject_02_01_2024.DTOs
{
    public class CustomerDeliveryAddressVM
    {
  
        public int Id { get; set; }
        public string? CusDelAddCode { get; set; }
        public string? CustomerCode { get; set; } = string.Empty;
        public string? ContactPerson { get; set; } = string.Empty;
        public string? CPPhoneNo { get; set; } = string.Empty;
        public string? CPAddress { get; set; } = string.Empty;
    }
}
