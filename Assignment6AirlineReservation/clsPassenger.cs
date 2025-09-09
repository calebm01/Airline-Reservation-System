using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// passenger object that contains values for a passenger from the database
    /// </summary>
    internal class clsPassenger
    {
        //PassengerID, FirstName, LastName

        /// <summary>
        /// unique identifier for passengers
        /// </summary>
        public string PassengerID;


        /// <summary>
        /// passenger's first name
        /// </summary>
        public string FirstName;


        /// <summary>
        /// passenger's last name
        /// </summary>
        public string LastName;

        /// <summary>
        /// passenger's seat number
        /// </summary>
        public string SeatNum;

        /// <summary>
        /// override tostring method for proper display of information in combo boxes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return FirstName + " " + LastName;
            }

            catch
            {
                return "could not receive passenger";
            }
            
        }

    }
}
