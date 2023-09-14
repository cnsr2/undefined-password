namespace undefined_password.Models
{
    public class User
    {
        public int UserId { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
