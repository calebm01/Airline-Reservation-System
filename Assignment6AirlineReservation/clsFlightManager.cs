using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Our class for accessing the database, getting flight data, and putting that flight data into a list
    /// </summary>
    public class clsFlightManager
    {
        /// <summary>
        /// database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// creating an object to access the database
        /// </summary>
        public clsFlightManager()
        {
            db = new clsDataAccess();
        }

        /// <summary>
        /// list method to access the flights table using our SQL class, getting that data, and putting it into our list of flights
        /// </summary>
        /// <returns> lstFlights </returns>
        public List<clsFlight> GetFlights()
        {
            DataSet ds = new DataSet();
            List<clsFlight> lstFlights = new List<clsFlight>();
            int iRet = 0;

            string sSQL = clsSQL.GetFlights();//////////////////////////////////////////////////Get the SQL statement that will get all the flights here from the clsSQL class
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                clsFlight flight = new clsFlight();
                flight.sFlightID = dr[0].ToString();////////////////////Make sure to use the example code I gave you, there are several database examples for how to extract data.
                flight.FlightNumber = dr[1].ToString();
                flight.AircraftType = dr[2].ToString();

                lstFlights.Add(flight);
            }

            return lstFlights;


        }
    }
}
