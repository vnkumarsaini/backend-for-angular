using NationalParkAPI_01.Models;

namespace NationalParkAPI_01.Repository.IRepository
{
    public interface ITrailsRepository
    {
        ICollection<Trails> GetAllTrails();
        Trails GetTrails(int TrailsId);
        ICollection<Trails> GetTrailsInNationaPark(int NationaParkId);
        bool TrailsExists(int TrailsId);
        bool TrailsExists(string TrailsName);
        bool CreateTrails(Trails trails);
        bool UpdateTrails(Trails trails);
        bool DeleteTrails(Trails trails);
        bool Save();
    }
}
