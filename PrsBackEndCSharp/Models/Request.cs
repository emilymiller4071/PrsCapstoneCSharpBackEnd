using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PrsBackEndCSharp.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        [StringLength(80)]
        public string Justification { get; set; }

        [StringLength(80)]
        public string? RejectionReason { get; set; }

        [StringLength(20)]
        public string DeliveryMode { get; set; }

        public DateTime SubmittedDate { get; set; }

        public DateTime DateNeeded { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; }

        public int UserID { get; set; }

        //Relation property that ties Request object to a user
       [ForeignKey(nameof(UserID))]
        public User? User { get; set; }


        public const string STATUSNEW = "New";
        public const string STATUSINREVIEW = "Review";
        public const string STATUSAPPROVED = "Approved";
        public const string STATUSREJECTED = "Rejected";
        public const string STATUSREOPENED = "Reopened";

        

    }
}
