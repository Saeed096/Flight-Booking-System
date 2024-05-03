﻿using Flight_Booking_System.Enums;
using Flight_Booking_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flight_Booking_System.DTOs
{
    public class TicketDTO
    {
        [Required(ErrorMessage = "The Ticket Id is required")]

        public int Id { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "The Price  is required")]

        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Class  is required")]
        public Class Class { get; set; }

        [Required(ErrorMessage = "The Passenger Id is required")]

        public int? PassengerId { get; set; }


        [Required(ErrorMessage = "The SeatId  is required")]

        public int? SeatId { get; set; }

        [Required(ErrorMessage = "The FlightId  is required")]
        public int? FlightId { get; set; }

    }
}