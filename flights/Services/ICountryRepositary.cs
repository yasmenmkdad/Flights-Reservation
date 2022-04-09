using flights.Context;
using flights.Entity;
namespace flights.Services
{
    public interface IcountryRepositary
    {
        public List<country> GetAll();
        public country GetDetails(int id);
        public void Insert(country country);
        public void Updatecountry(int id, country country);
        public void Deletecountry(int id);
    }
}
