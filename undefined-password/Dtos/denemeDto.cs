namespace undefined_password.Dtos
{
    public class DenemeDto
    {
        public int UserId { get; set; }
        public List<ArrayDto> Passwords { get; set; }

    }
    public class ArrayDto
    {
        public int PasswordId { get; set; }
        public int CategoryId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordName { get; set; }
    }
}
