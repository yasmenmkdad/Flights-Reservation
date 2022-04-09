using flights.Context;
using flights.Entity;


namespace flights.Services
{
    public class airplaneRepoService : IairplaneRepositary
    {
        public FlightsystemdbContext Context { get; set; }
        public airplaneRepoService(FlightsystemdbContext context)
        {
            Context = context;
        }
        public void Deleteairplane(int id)
        {
            Context.Remove(Context.airplanes.Find(id));
            Context.SaveChanges();
        }

        public List<airplane> GetAll()
        {
            return Context.airplanes.ToList();
        }

        public airplane GetDetails(int id)
        {
            return Context.airplanes.Find(id);
        }

        public void Insert(airplane airplane)
        {
            Context.airplanes.Add(airplane);
            Context.SaveChanges();
        }

        public void Updateairplane(int id, airplane airplane)
        {
            airplane airplaneUpdated = Context.airplanes.Find(id);
            airplaneUpdated.TotalSeat = airplane.TotalSeat;
            airplaneUpdated.ModelNumber = airplane.ModelNumber;
            airplaneUpdated.FlightNumber = airplane.FlightNumber;
            Context.SaveChanges();
        }
    }
}
