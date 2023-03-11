using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrsBackEndCSharp.Models
{
    public class RequestLine
    {
        [JsonPropertyName("id")]
        [Key]
        public int ID { get; set; }

        public int RequestID { get; set; }
        
        public int ProductID { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        //relationship
       [JsonIgnore]  // breaks a cycle
       [ForeignKey(nameof(RequestID))]
        public Request? Request { get; set; }

        // relationship
        [ForeignKey(nameof(ProductID))]
        public Product? Product { get; set; }

    }
}
