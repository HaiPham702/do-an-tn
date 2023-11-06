using System.ComponentModel.DataAnnotations;

namespace ElectronicLibrary.Core.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBrith { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        public string UserName { get; set; }

        public int Role { get; set; }

        public string Token { get; set; }

        public bool IsActive { get; set; }
    }

    public class LoginUser
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegisterUser
    {
        public string Name { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "Everyone";
    }
}