using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace undefined_password.Models
{
    //[Keyless]
    [PrimaryKey(nameof(PasswordId))]
    public class CreatePassword
    {
        public int PasswordId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordName { get; set; }
    }
}
