using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PrsBackEndCSharp.Models
{
    public class Product
    {
        [Key] 
        public int Id { get; set; }

        [StringLength(30)]
        public string PartNbr { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }

        [StringLength(30)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }

        public int VendorID { get; set; }




    }
}
