using System.ComponentModel.DataAnnotations;

namespace C__tutorials.Models
{
#pragma warning disable
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }

        public string PasswordSalt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string CreditCard { get; set; }
        public int Age { get; set; }
    }
}