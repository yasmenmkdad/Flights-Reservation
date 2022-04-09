using flights.Context;
using flights.Entity;

namespace flights.Services
{
    public interface IflightRepositary
    {
        public List<flight> GetAll();
        public flight GetDetails(int id);
        public void Insert(flight flight);
        public void Updateflight(int id, flight flight);
        public void Deleteflight(int id);
    }
}
