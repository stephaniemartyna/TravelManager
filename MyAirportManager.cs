using System.IO;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using Traveless.Manager.Abstract;
using Traveless.Manager.Entities;

namespace Traveless.Manager
{
    /// <summary>
    /// Manages airports
    /// </summary>
    public class MyAirportManager : AirportManager
    {
        /// <summary>
        /// Path to airports.csv file
        /// </summary>
        public static readonly string AIRPORTS_FILE = "Data/airports.csv";

        /// <summary>
        /// Populates list with Airport instances from .csv file.
        /// </summary>
        protected override void LoadAirports()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AIRPORTS_FILE);
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cells = line.Split(',');
                    string code = cells[0];
                    string name = cells[1];

                    foreach (string cell in cells)
                    {
                        Airport airport = new Airport(code, name);
                        _airports.Add(airport);
                    }
                   
                }
            reader.Close();
            }
        }
    }
}


