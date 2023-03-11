using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PrsBackEndCSharp.Models
{
    public class Product
    {
        [Key] 
        public int ID { get; set; }

        [StringLength(30)]
        [Required]
        public string PartNbr { get; set; }

        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }

        [StringLength(30)]
        [Required]
        public string Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }

        [Required]
        public int VendorID { get; set; }

        [ForeignKey(nameof(VendorID))]
        public Vendor? Vendor { get; set; }


    }
}
