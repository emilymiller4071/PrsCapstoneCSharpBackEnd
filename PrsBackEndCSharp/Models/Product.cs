using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PrsBackEndCSharp.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        [Key] 
        public int ID { get; set; }

        [JsonPropertyName("partNbr")]
        [StringLength(30)]
        [Required]
        public string PartNbr { get; set; }

        [JsonPropertyName("name")]
        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }

        [JsonPropertyName("unit")]
        [StringLength(30)]
        [Required]
        public string Unit { get; set; }

        [JsonPropertyName("photoPath")]
        [StringLength(255)]
        public string? PhotoPath { get; set; }

        [JsonPropertyName("vendorId")]
        [Required]
        public int VendorID { get; set; }

        [ForeignKey(nameof(VendorID))]
        public Vendor? Vendor { get; set; }


    }
}
