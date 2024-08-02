using NationalParkAPI_01.Models;

namespace NationalParkAPI_01.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(int NationalParkId);
        bool NationalParkExists(int NationalParkId);
        bool NationalParkExists(string NationalParkName);
        bool CreateNationalPark(NationalPark nationalpark);
        bool UpdateNationalPark(NationalPark nationalpark);
        bool DeleteNationalPark(NationalPark nationalpark);
        bool Save();
    }
}
