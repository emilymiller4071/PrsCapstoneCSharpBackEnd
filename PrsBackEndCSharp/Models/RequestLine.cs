using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrsBackEndCSharp.Models
{
    public class RequestLine
    {
        [Key]
        public int ID { get; set; }

        public int RequestID { get; set; }
        
        public int ProductID { get; set; }

        public int Quantity { get; set; }

        //relationship
       [JsonIgnore]  // breaks a cycle
       [ForeignKey(nameof(RequestID))]
        public Request? Request { get; set; }

        // relationship
        [ForeignKey(nameof(ProductID))]
        public Product? Product { get; set; }

        [JsonIgnore]
        public List<RequestLine>? GetAllRequestLines { get; set; }

    }
}
