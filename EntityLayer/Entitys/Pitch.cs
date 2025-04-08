using EntityLayer.Enums;

namespace EntityLayer.Entitys
{
    public class Pitch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
    }
}