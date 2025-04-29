using EntityLayer.Entitys;

namespace BusinessLayer.Interfaces
{
    public interface ICityApiServices
    {
        Task<List<Citys>> GetCitiesAsync();
    }
}
