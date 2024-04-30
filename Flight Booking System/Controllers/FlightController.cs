﻿using Flight_Booking_System.Context;
using Flight_Booking_System.DTOs;
using Flight_Booking_System.Models;
using Flight_Booking_System.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Flight_Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ITIContext _context;

        public FlightController(ITIContext ITIContext)
        {
            _context = ITIContext;
        }

        //***********************************************

        [HttpGet]
        public ActionResult<GeneralResponse> Get()
        {
            List<Flight> flights = _context.Flights.ToList();

            List<FlightDTO> flightDTOs = new List<FlightDTO>();

            foreach (Flight flight in flights)
            {
                FlightDTO flightDTO = new FlightDTO()
                {
                    Id = flight.Id,

                    Start = flight.Start,
                    Destiantion = flight.Destiantion,

                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,

                    PlaneId = flight.PlaneId,
                };

                flightDTOs.Add(flightDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = flightDTOs , 
                Message = "All flights"
            };
        }

        [HttpGet("{id:int}")] // from route 
        public ActionResult<GeneralResponse> GetbyId(int id)
        {
            Flight? flightFromDB = _context.Flights.FirstOrDefault(flight => flight.Id == id);

            if (flightFromDB == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID"
                };
            }
            else
            {
                FlightDTO flightDTO = new FlightDTO()
                {
                    Id = flightFromDB.Id,

                    Start = flightFromDB.Start,
                    Destiantion = flightFromDB.Destiantion,

                    DepartureTime = flightFromDB.DepartureTime,
                    ArrivalTime = flightFromDB.ArrivalTime,

                    PlaneId = flightFromDB.PlaneId,
                };

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = flightDTO,

                    Message = "Found"
                };
            }
        }


        [HttpPost]
        public ActionResult<GeneralResponse> Add(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);

                _context.SaveChanges();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = flight,

                    Message = "Flight Added Successfully",
                };

                // omar : we can use one od them if we needed but I think the General Response is better 
                #region possible return responses

                //return NoContent();

                //return Created($"http://localhost:40640/api/flight{flight.Id}", flight);

                //return CreatedAtAction("GetById", new { id = flight.Id }, flight); 
                #endregion
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = ModelState,

                    Message = "the Model State is not valid"
                };
            }
        }

        [HttpPut]
        public ActionResult<GeneralResponse> Edit(int id, Flight editedFlight)
        {
            Flight? flightFromDB = _context.Flights.FirstOrDefault(f => f.Id == id);

            if (flightFromDB == null || editedFlight.Id != id)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID or the IDs are not matched , \n" +
                    " make sure that the original flight ID matches the edited flight ID .",
                };
            }
            else
            {
                _context.Update(editedFlight);

                _context.SaveChanges();

                return new GeneralResponse()
                {
                    IsSuccess = true,

                    Data = editedFlight,

                    Message = "Flight Edited Successfully",
                };

                // omar : we can use one od them if we needed but I think the General Response is better 
                #region possible return responses
                // omar : possible return responses :

                //return NoContent();

                //return Created($"http://localhost:40640/api/flight{editedFlight.Id}", editedFlight);

                //return CreatedAtAction("GetById", new { id = editedFlight.Id }, editedFlight); 
                #endregion
            }
        }

        [HttpDelete("{id:int}")] // from route
        public ActionResult<GeneralResponse> Delete(int id)
        {
            Flight? flightFromDB = _context.Flights.FirstOrDefault(flight => flight.Id == id);

            if (flightFromDB == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,

                    Data = null,

                    Message = "No Flight Found with this ID",
                };
            }
            else
            {
                try
                {
                    _context.Remove(id);

                    _context.SaveChanges();

                    return new GeneralResponse()
                    {
                        IsSuccess = true,

                        Data = null,

                        Message = "Flight Edited Successfully",
                    };

                    // omar : possible return response :
                    //return NoContent();
                }
                catch (Exception ex)
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,

                        Data = null,

                        Message = ex.Message,
                    };
                }
            }
        }
    }
}
