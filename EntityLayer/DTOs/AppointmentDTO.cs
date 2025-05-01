using EntityLayer.Entitys;

namespace EntityLayer.DTOs
{
    public class AppointmentDTO
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int CitysId { get; set; }
        public int PitchId { get; set; }
        public string SelectedTime { get; set; }
    }
}