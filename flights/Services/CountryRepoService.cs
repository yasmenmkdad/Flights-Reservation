using flights.Context;
using flights.Entity;

namespace flights.Services
{
    public class countryRepoService : IcountryRepositary
    {
        public FlightsystemdbContext Context { get; set; }
        public countryRepoService(FlightsystemdbContext context)
        {
            Context = context;
        }
        public void Deletecountry(int id)
        {
            Context.Remove(Context.countries.Find(id));
            Context.SaveChanges();
        }

        public List<country> GetAll()
        {
            return Context.countries.ToList();
        }

        public country GetDetails(int id)
        {
            return Context.countries.Find(id);
        }

        public void Insert(country country)
        {
            Context.countries.Add(country);
            Context.SaveChanges();
        }

        public void Updatecountry(int id, country country)
        {
            country countryUpdated = Context.countries.Find(id);
            countryUpdated.Name = country.Name;
            countryUpdated.Image = country.Image;

            Context.SaveChanges();
        }
    }
}
