using EntityLayer.Entitys;

namespace BusinessLayer.Interfaces
{
    public interface IPitchApiServices
    {
        Task<List<Pitch>> GetPitchesAsync();
    }
}
