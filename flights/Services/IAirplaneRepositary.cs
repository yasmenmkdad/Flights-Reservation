using flights.Context;
using flights.Entity;

namespace flights.Services
{
    public interface IairplaneRepositary
    {
        public List<airplane> GetAll();
        public airplane GetDetails(int id);
        public void Insert(airplane airplane);
        public void Updateairplane(int id, airplane airplane);
        public void Deleteairplane(int id);
    }
}
