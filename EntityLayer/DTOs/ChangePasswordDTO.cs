using System.ComponentModel.DataAnnotations;

namespace EntityLayer.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
        public string CurrentPassword { get; set; }
        
        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [MinLength(10, ErrorMessage = "Şifre en az 10 karakter olmalıdır.")]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmNewPassword { get; set; }
    }
}