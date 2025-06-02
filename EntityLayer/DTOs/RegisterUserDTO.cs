using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        [MinLength(10)]
        public string Password { get; set; }
    }
}