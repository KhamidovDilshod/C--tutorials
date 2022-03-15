using System.ComponentModel.DataAnnotations;

namespace C__tutorials.Models
{
#pragma warning disable
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public string PasswordSalt { get; set; }

        //Additional properties after registration
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string CreditCard { get; set; }
        public int Age { get; set; }
    }
}