namespace EntityLayer.Entitys
{
    public class Citys
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Appointment> Appointments { get; set; } // Added this property
    }
}
