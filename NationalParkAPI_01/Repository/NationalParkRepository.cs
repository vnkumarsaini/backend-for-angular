using NationalParkAPI_01.Data;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Repository.IRepository;

namespace NationalParkAPI_01.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;
        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateNationalPark(NationalPark nationalpark)
        { 
            _context.NationalPark.Add(nationalpark);
           return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalpark)
        {
            _context.NationalPark.Remove(nationalpark);
            return Save();
        }

        public NationalPark GetNationalPark(int NationalParkId)
        {
           return _context.NationalPark.Find(NationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalPark.ToList();
        }

        public bool NationalParkExists(int NationalParkId)
        {
           return _context.NationalPark.Any(np=>np.Id == NationalParkId);
        }

        public bool NationalParkExists(string NationalParkName)
        {
            return _context.NationalPark.Any(np => np.Name == NationalParkName);
        }
        public bool Save()
        {
          return _context.SaveChanges() == 1 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalpark)
        {
            _context.NationalPark.Update(nationalpark);
            return Save();
        }
    }
}
