using flights.Context;
using flights.Entity;

namespace flights.Services
{
    public class TicketRepoService : ITicketRepositary
    {
        public FlightsystemdbContext Context { get; set; }
        public TicketRepoService(FlightsystemdbContext context)
        {
            Context = context;
        }
        public void DeleteTicket(int id)
        {
            Context.Remove(Context.tickets.Find(id));
            Context.SaveChanges();
        }

        public List<ticket> GetAll()
        {
            return Context.tickets.ToList();
        }

        public ticket GetDetails(int id)
        {
            return Context.tickets.Find(id);
        }

        public void Insert(ticket ticket)
        {
            Context.tickets.Add(ticket);
            Context.SaveChanges();
        }

        public void UpdateTicket(int id, ticket ticket)
        {
            ticket ticketUpdated = Context.tickets.Find(id);
            ticketUpdated.NoOfSeats = ticket.NoOfSeats;
            ticketUpdated.CheckStatus = ticket.CheckStatus;
            ticketUpdated.UserID = ticket.UserID;
            ticketUpdated.FlightNumber = ticket.FlightNumber;
           

            Context.SaveChanges();
        }
    }
}
