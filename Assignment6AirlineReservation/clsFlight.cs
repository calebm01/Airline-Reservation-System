using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// flight object that contains values for a flight from the database
    /// </summary>
    public class clsFlight
    {
        //sFlightID, FlightNumber, AircraftType

        /// <summary>
        /// Flight ID from the database
        /// </summary>
        public string sFlightID;

        /// <summary>
        /// Flight Number from the database
        /// </summary>
        public string FlightNumber;

        /// <summary>
        /// Type of Plane from the database
        /// </summary>
        public string AircraftType;

        //toString override

        public override string ToString()
        {
            try
            {
                return AircraftType + " Flight " + FlightNumber;
            }
            catch
            {
                return "could not receive flight";
            }
        }
    }
}
