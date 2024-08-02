using Microsoft.EntityFrameworkCore;
using NationalParkAPI_01.Data;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Repository.IRepository;

namespace NationalParkAPI_01.Repository
{
    public class TrailsRepository : ITrailsRepository
    {
        private readonly ApplicationDbContext _context;
        public TrailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateTrails(Trails trails)
        {
            _context.Trails.Add(trails);
            return Save();
        }

        public bool DeleteTrails(Trails trails)
        {
            _context.Trails.Remove(trails);
            return Save();
        }

        public ICollection<Trails> GetAllTrails()
        {
           return _context.Trails.Include(t=>t.NationalPark).ToList();
        }

        public Trails GetTrails(int TrailsId)
        {
            return _context.Trails.Find(TrailsId);
        }

        public ICollection<Trails> GetTrailsInNationaPark(int NationaParkId)
        {
           return _context.Trails.Include(t=>t.NationalParkId==NationaParkId).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool TrailsExists(int TrailsId)
        {
            return _context.Trails.Any(np => np.Id == TrailsId);
        }

        public bool TrailsExists(string TrailsName)
        {
            return _context.Trails.Any(np => np.Name == TrailsName);
        }

        public bool UpdateTrails(Trails trails)
        {
            _context.Trails.Update(trails);
            return Save();
        }
    }
}
