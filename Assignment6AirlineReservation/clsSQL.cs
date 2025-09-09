using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsSQL
    {

        /// <summary>
        /// a method representing SQL statement needed to get a flight out of the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// a method representing SQL statement needed to get passengers out of the database
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                              "FROM Passenger, Flight_Passenger_Link FPL " +
                              "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                              "Flight_ID = " + sFlightID;

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// a method representing SQL statement needed to update seat numbers in the database
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="SeatNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateSeat(string sFlightID, string sPassengerID, string SeatNum)
        {
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                              "SET Seat_Number =  '"+SeatNum+"'" +
                              "WHERE FLIGHT_ID = " + sFlightID + " AND PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// a method representing SQL statement needed to add passenger to the database
        /// </summary>
        /// <param name="Fname"></param>
        /// <param name="Lname"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string AddPassenger(string Fname, string Lname)
        {
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('"+Fname+"','"+Lname+"')";

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// a method representing SQL statement needed to add a flight link object to that table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="SeatNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string AddFlightLink(string sFlightID, string sPassengerID, string SeatNum)
        {
            try
            {
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                              "VALUES( "+sFlightID+" , "+sPassengerID+" , '"+SeatNum+"')";

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// a method representing SQL statement needed to delete a passenger from the database
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassenger(string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER " +
                              "WHERE PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// a method representing SQL statement needed to delete a flight link object from that table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="SeatNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteFlightLink(string sFlightID, string sPassengerID)
        {
            try
            {
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                              "WHERE FLIGHT_ID = " + sFlightID +" AND " +
                              "PASSENGER_ID = " +sPassengerID+"";

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// getting the passenger ID after adding one to the database, this is to insert that same passenger  into the flight link table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengerID(string Fname, String Lname)
        {
            try
            {
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '"+Fname+ "' AND Last_Name = '"+Lname+"'";

                return sSQL;
            }

            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                     MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}

