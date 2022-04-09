using flights.Context;
using flights.Entity;


namespace flights.Services
{
    public interface IAirlineRepositary
    {
        public List<airline> GetAll();
        public airline GetDetails(int id);
        public void Insert(airline airline);
        public void UpdateAirline(int id, airline airline);
        public void DeleteAirline(int id);
    }
}
