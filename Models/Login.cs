namespace C__tutorials.Models
{
#pragma warning disable
    public class Login
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string? token { get; set; }
        public Login(string Email, string Role)
        {
            this.Email = Email;
            this.Role = Role;
        }
    }
}