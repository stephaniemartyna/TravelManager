using System;
using System.Collections.Generic;
using System.IO;
using Traveless.Manager.Abstract;
using Traveless.Manager.Entities;

namespace Traveless.Manager
{
    /// <summary>
    /// Manages flights
    /// </summary>
    public class MyFlightManager : FlightManager
    {
        /// <summary>
        /// Path to flights.csv file
        /// </summary>
        public static readonly string FLIGHTS_FILE = "Data/flights.csv";

        /// <summary>
        /// Populates list with Flight instances from file
        /// </summary>
        protected override void LoadFlights()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FLIGHTS_FILE);
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    string[] cells = line.Split(',');
                    if (cells.Length != 7 ) 
                    {
                        continue;
                    }
                    Flight flight = new Flight(cells[0], cells[1], cells[2], cells[3], cells[4], int.Parse(cells[5]), decimal.Parse(cells[6]));
                    _flights.Add(flight);
                   
                }
            }
        }

        /// <summary>
        /// Finds flight with code
        /// </summary>
        /// <param name="code">Flight code argument</param>
        /// <returns>Flight instance (or null if not found)</returns>
        public override Flight? FindFlightByCode(string code)
        {
            foreach (Flight flight in Flights)
            {
                if (flight.Code == code)
                    { return flight; }
                else
                    { break; }
            }
            return null;
        }
    }
}
