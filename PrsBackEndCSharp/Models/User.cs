using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrsBackEndCSharp.Models
{
    public class User   // POCO
    {
        [JsonPropertyName("id")]
        [Key]
        public int ID { get; set; }

        [JsonPropertyName("userName")]
        [StringLength(30)]
        [Required]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        [StringLength(30)]
        [Required]
        public string Password { get; set; }

        [JsonPropertyName("firstName")]
        [StringLength(30)]
        [Required]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        [StringLength(30)]
        [Required]
        public string LastName { get; set; }

        [JsonPropertyName("phone")]
        [StringLength(12)]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        [StringLength(255)]
        public string? Email { get; set; }

        [JsonPropertyName("reviewer")]
        [Required]
        public bool IsReviewer { get; set; }

        [JsonPropertyName("admin")]
        [Required]
        public bool IsAdmin { get; set; }






    }
}
