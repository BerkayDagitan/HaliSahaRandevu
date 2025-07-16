using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entitys
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}