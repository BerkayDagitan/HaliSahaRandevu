using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }
        [Required]
        [MinLength(10)]
        public string Password { get; set; }
    }
}