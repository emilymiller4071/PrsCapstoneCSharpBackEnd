using System.ComponentModel.DataAnnotations;

namespace PrsBackEndCSharp.Models
{
    public class RequestLine
    {
        [Key]
        public int Id { get; set; }

        public int RequestID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }


    }
}
