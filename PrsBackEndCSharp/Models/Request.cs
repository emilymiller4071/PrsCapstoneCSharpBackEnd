using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PrsBackEndCSharp.Models
{
    public class Request
    {
        [JsonPropertyName("id")]
        [Key]
        public int ID { get; set; }

        [JsonPropertyName("description")]
        [StringLength(80)]
        public string Description { get; set; }

        [JsonPropertyName("justification")]
        [StringLength(80)]
        public string Justification { get; set; }

        [JsonPropertyName("rejectionReason")]
        [StringLength(80)]
        public string? RejectionReason { get; set; }

        [JsonPropertyName("deliveryMode")]
        [StringLength(20)]
        public string DeliveryMode { get; set; }

        [JsonPropertyName("submittedDate")]
        public DateTime SubmittedDate { get; set; }

        [JsonPropertyName("dateNeeded")]
        public DateTime DateNeeded { get; set; }

        [JsonPropertyName("status")]
        [StringLength(10)]
        public string Status { get; set; }

        [JsonPropertyName("total")]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; }

        [JsonPropertyName("userId")]
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
