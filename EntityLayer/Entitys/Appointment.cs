using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entitys
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CitysId { get; set; }
        public Citys Citys { get; set; }

        public int PitchId { get; set; }
        public Pitch Pitch { get; set; }
    }
}