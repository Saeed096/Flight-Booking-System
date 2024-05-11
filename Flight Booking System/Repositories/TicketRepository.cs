﻿using Flight_Booking_System.Context;
using Flight_Booking_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Flight_Booking_System.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(ITIContext _context) : base(_context)
        {

        }

        //*************************************************************

        public Ticket? GetWithSeat_Passenger(int? id)
        {
          return  Context.Tickets.Where(t => t.Id == id).Include(t => t.Seat).Include(t => t.Passenger).FirstOrDefault();
        }

        public List<Ticket> GetWithSeatByFlightId(int id) 
        {
            return Context.Tickets.Where(t => t.FlightId == id).Include(t => t.Seat).ToList();
        }
    }
}
