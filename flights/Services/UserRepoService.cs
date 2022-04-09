using flights.Context;
using flights.Entity;


namespace flights.Services
{
    public class UserRepoService : IUserRepositary
    {
        public FlightsystemdbContext Context { get; set; }
        public UserRepoService(FlightsystemdbContext context)
        {
            Context = context;
        }
        public void DeleteUser(int id)
        {
            Context.Remove(Context.AspNetUsers.Find(id));
            Context.SaveChanges();
        }

        public List<AspNetUser> GetAll()
        {
            return Context.AspNetUsers.ToList();
        }

        public AspNetUser GetDetails(int id)
        {
            return Context.AspNetUsers.Find(id);
        }

        public void Insert(AspNetUser user)
        {
            Context.AspNetUsers.Add(user);
            Context.SaveChanges();
        }

        public void UpdateUser(int id, AspNetUser user)
        {
            AspNetUser userUpdated = Context.AspNetUsers.Find(id);
            userUpdated.User_Name = user.User_Name;
            userUpdated.phone = user.phone;
            userUpdated.Address = user.Address;
            userUpdated.age = user.age;
            userUpdated.Email = user.Email;
            userUpdated.PasswordHash = user.PasswordHash;
            userUpdated.gender = user.gender;
            userUpdated.card_number = user.card_number;
            userUpdated.passport=user.passport;
            
        }

    }
}
