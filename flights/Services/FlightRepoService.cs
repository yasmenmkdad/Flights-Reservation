using flights.Context;
using flights.Entity;

namespace flights.Services
{
    public class flightRepoService : IflightRepositary
    {
        public FlightsystemdbContext Context { get; set; }
        public flightRepoService(FlightsystemdbContext context)
        {
            Context = context;
        }
        public void Deleteflight(int id)
        {
            Context.Remove(Context.flights.Find(id));
            Context.SaveChanges();
        }

        public List<flight> GetAll()
        {
            return Context.flights.ToList();
        }

        public flight GetDetails(int id)
        {
            return Context.flights.Find(id);
        }

        public void Insert(flight flight)
        {
            Context.flights.Add(flight);
            Context.SaveChanges();
        }

        public void Updateflight(int id, flight flight)
        {
            flight flightUpdated = Context.flights.Find(id);
            flightUpdated.From = flight.From;
            flightUpdated.To = flight.To;
            flightUpdated.DepartureTime = flight.DepartureTime;
            flightUpdated.ArrivalTime = flight.ArrivalTime;
            flightUpdated.AvailableSeat = flight.AvailableSeat;
            flightUpdated.Price = flight.Price;
            flightUpdated.CountryID = flight.CountryID;
            Context.SaveChanges();
        }
    }
}
