using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrsBackEndCSharp.Models
{
    public class Vendor
    {
        [JsonPropertyName("id")]
        [Key]
        public int ID { get; set; }

        [JsonPropertyName("code")]
        [StringLength(30)]
        [Required]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        [StringLength(30)]
        [Required]
        public string Address { get; set; }

        [JsonPropertyName("city")]
        [StringLength(30)]
        [Required]
        public string City { get; set; }

        [JsonPropertyName("state")]
        [StringLength(2)]
        [Required]
        public string State { get; set; }

        [JsonPropertyName("zip")]
        [StringLength(5)]
        [Required]
        public string Zip { get; set; }

        [JsonPropertyName("phone")]
        [StringLength(12)]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        [StringLength(255)]
        public string? Email { get; set;}



    }
}
