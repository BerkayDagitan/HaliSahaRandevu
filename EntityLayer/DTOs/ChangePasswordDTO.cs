using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [MinLength(10)]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}