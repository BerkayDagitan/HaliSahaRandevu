using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entitys
{
    public class Pitch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CitysId { get; set; }
        public Citys Citys { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}