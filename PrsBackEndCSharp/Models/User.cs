using System.ComponentModel.DataAnnotations;

namespace PrsBackEndCSharp.Models
{
    public class User   // POCO
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(30)]
        public string Password { get; set; }
       
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [StringLength(12)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        public bool IsReviewer { get; set; }

        public bool IsAdmin { get; set; }






    }
}
