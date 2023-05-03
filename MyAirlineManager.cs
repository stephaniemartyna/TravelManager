using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using Traveless.Manager.Abstract;
using Traveless.Manager.Entities;

namespace Traveless.Manager
{
    /// <summary>
    /// Manages airlines
    /// </summary>
    public class MyAirlineManager : AirlineManager
    {
        /// <summary>
        /// Path to airlines.csv file
        /// </summary>
        public static readonly string AIRLINES_FILE = "Data/airlines.csv";

        /// <summary>
        /// Populate list with Airline instances from CSV files
        /// </summary>
        protected override void LoadAirlines()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AIRLINES_FILE);
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cells = line.Split(',');
                    if (cells.Length != 2) { continue; }
                    //foreach (string cell in cells)
                    //{

                    //}
                    string code = cells[0];
                    string name = cells[1];

                    Airline airline = new Airline(code, name);
                    _airlines.Add(airline);
                    
                }
               reader.Close();
            }
        }
    }
}
