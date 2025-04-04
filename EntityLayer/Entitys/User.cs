namespace EntityLayer.Entitys
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}