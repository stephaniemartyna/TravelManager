using Traveless.Manager.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;
using Traveless.Manager.Exceptions;
using Traveless.Manager.Abstract;

namespace Traveless.Manager
{
    /// <summary>
    /// Manages reservations
    /// </summary>
    public class MyReservationManager : ReservationManager
    {
        /// <summary>
        /// Makes a reservation
        /// </summary>
        /// <param name="flight">Flight to apply reservation to</param>
        /// <param name="name">Name</param>
        /// <param name="citizenship">Citizenship</param>
        /// <returns>Created Reservation instance</returns>
        /// <exception cref="MakeReservationException">Thrown if unable to make reservation</exception>
        public override Reservation MakeReservation(Flight flight, string name, string citizenship)
        {
            if (flight == null || name == null ||  citizenship == null)
            {
                throw new MakeReservationException();
            }
            string code = Reservation.GenerateReservationCode(flight);
            Reservation reservation = new Reservation(code,flight,name,citizenship,true);
            _reservations.Add(reservation);
            return reservation;
        }

        /// <summary>
        /// Finds reservation with code
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>Reservation instance or null if not found</returns>
        public override Reservation? FindReservationByCode(string code)
        {
            foreach (Reservation reservation in _reservations)
            {
                 if (reservation.Code == code)
                {
                    return reservation;
                }
                 else
                {
                    break;
                }
            }
            return null;
        }

        /// <summary>
        /// Updates an existing reservation
        /// </summary>
        /// <param name="code">Code of existing reservation</param>
        /// <param name="name">Name to change reservation to</param>
        /// <param name="citizenship">Citizenship to change reservation to</param>
        /// <param name="isActive">Whether reservation is active or inactive</param>
        /// <exception cref="UpdateReservationException">Thrown if unable to update reservation</exception>
        public override void Update(string code, string name, string citizenship, bool isActive)
        {
            if (code == null)
            { throw new MakeReservationException(); }
            isActive = false;
            foreach (Reservation reservation in _reservations) 
            {
                if (reservation.Code == code) 
                {
                    name = reservation.Name;
                    isActive = reservation.IsActive;
                    isActive = true;
                    break;
                }
            }
            throw new UpdateReservationException();
        }

        /// <summary>
        /// Determines number of available seats for flight
        /// </summary>
        /// <param name="flight">Flight instance</param>
        /// <returns>Number of available seats</returns>
        public override int AvailableSeats(Flight flight)
        {
            int flightCount = 0;
            foreach (Reservation reservation in _reservations)
            {
                if (reservation.Flight == flight && reservation.IsActive) 
                {
                     
                    flightCount++;
                }
            }
            int availableseats = flight.TotalSeats - flightCount;
            return availableseats;
        }
    }
}
