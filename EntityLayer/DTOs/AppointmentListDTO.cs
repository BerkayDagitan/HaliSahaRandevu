using EntityLayer.Entitys;

namespace EntityLayer.DTOs
{
    public class AppointmentListDTO
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; }
        public string CitysName { get; set; }
        public string PitchName { get; set; }
    }
}