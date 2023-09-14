namespace undefined_password.Dtos
{
    public class CreatePasswordDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = "";
        public string PasswordName { get; set; }
        public bool isActive { get; set; } = true;
        public bool _lowerCase { get; set; }
        public bool _upperCase { get; set; } 
        public bool _symbols { get; set; }
        public bool _numbers { get; set; }
        public int _length { get; set; } = 8;
    }
}
