using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// our class for pulling passenger data out of the database and inserting that data into a list
    /// </summary>
    internal class clsPassengerManager
    {
        /// <summary>
        /// database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Our list of passengers
        /// </summary>
        public static List<clsPassenger> passengers = new List<clsPassenger>();

        /// <summary>
        /// creating an object to access the database
        /// </summary>
        public clsPassengerManager()
        {
            db = new clsDataAccess();
        }

        /// <summary>
        /// Getting our data and looping through it and putting that data into a list, the lists change based on the FlightID
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns> lstPassengers </returns>
        public List<clsPassenger> GetPassengers(string sFlightID)
        {
            try
            {
                DataSet ds = new DataSet();
                List<clsPassenger> lstPassengers = new List<clsPassenger>();
                int iRet = 0;

                string sSQL = clsSQL.GetPassengers(sFlightID);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassenger passenger = new clsPassenger();
                    passenger.PassengerID = dr[0].ToString();
                    passenger.FirstName = dr[1].ToString();
                    passenger.LastName = dr[2].ToString();
                    passenger.SeatNum = dr[3].ToString();

                    lstPassengers.Add(passenger);
                }

                return lstPassengers;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Adding a passenger to the database
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="LName"></param>
        /// <param name="SeatNum"></param>
        public void AddPassenger(String FName, String LName)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsSQL.AddPassenger(FName, LName);
                //ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                iRet = db.ExecuteNonQuery(sSQL);//////////////////////////////////////////////////////Need to use this method since you are inserting records and not retreiving them
            }

            catch (Exception ex)
            {
                
            }

        }

        /// <summary>
        /// Adding a flight link to the database
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="SeatNUM"></param>
        public void AddFlightLink(String sFlightID, String sPassengerID, String SeatNUM) 
        
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsSQL.AddFlightLink(sFlightID, sPassengerID, SeatNUM);
                iRet = db.ExecuteNonQuery(sSQL);
            }

            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Deleting a flight link from the database
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        public void DeleteFlightLink(String sFlightID, String sPassengerID)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsSQL.DeleteFlightLink(sFlightID, sPassengerID);
                iRet = db.ExecuteNonQuery(sSQL);
            }

            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Deleting a passenger from the database
        /// </summary>
        /// <param name="sPassengerID"></param>
        public void DeletePassenger(String sPassengerID)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsSQL.DeletePassenger(sPassengerID);
                iRet = db.ExecuteNonQuery(sSQL);
            }

            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Updating a seat in the database
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <param name="sFlightID"></param>
        /// <param name="SeatNum"></param>
        public void UpdateSeat(String sPassengerID, String sFlightID, String SeatNum)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsSQL.UpdateSeat(sFlightID, sPassengerID, SeatNum);
                iRet = db.ExecuteNonQuery(sSQL);
            }

            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// getting the passenger id, purpose is to add a value to the FlightLink table
        /// </summary>
        /// <param name="Fname"></param>
        /// <param name="Lname"></param>
        /// <returns></returns>
        public string GetPassengerID(String Fname, String Lname)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;
                string PassID;

                string sSQL = clsSQL.GetPassengerID(Fname, Lname);
                //ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                PassID = db.ExecuteScalarSQL(sSQL);///////////////////////////////////////////////Use execute scalar to get one value
                //PassID = ds.Tables[0].Rows[0].ToString();

                return PassID;
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
