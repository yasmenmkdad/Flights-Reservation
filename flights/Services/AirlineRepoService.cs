using flights.Context;
using flights.Entity;

namespace flights.Services
{
    public class AirlineRepoService : IAirlineRepositary
    {
        public FlightsystemdbContext Context { get; set; }
        public AirlineRepoService(FlightsystemdbContext context)
        {
            Context = context;
        }
        public void DeleteAirline(int id)
        {
            Context.Remove(Context.airlines.Find(id));
            Context.SaveChanges();
        }

        public List<airline> GetAll()
        {
            return Context.airlines.ToList();
        }

        public airline GetDetails(int id)
        {
            return Context.airlines.Find(id);
        }

        public void Insert(airline airline)
        {
            Context.airlines.Add(airline);
            Context.SaveChanges();
        }

        public void UpdateAirline(int id, airline airline)
        {
            airline airlineUpdated = Context.airlines.Find(id);
            airlineUpdated.Name = airline.Name;
            airlineUpdated.Phone = airline.Phone;
            airlineUpdated.RegisterationNumber = airline.RegisterationNumber;
            airlineUpdated.Address = airline.Address;

            Context.SaveChanges();
        }
    }
}
