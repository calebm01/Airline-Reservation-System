using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // clsDataAccess clsData;

        /// <summary>
        /// add passenger window
        /// </summary>
        wndAddPassenger wndAddPass;

        /// <summary>
        /// clsFlightManager object
        /// </summary>
        clsFlightManager Myflight;

        /// <summary>
        /// clsPassengerManager object
        /// </summary>
        clsPassengerManager MyPassenger;

        /// <summary>
        /// variable to tell the program if a passenger is currently being added
        /// </summary>
        bool bAddPassengerMode;

        /// <summary>
        /// variable to tell the program if a seat is currently being changed
        /// </summary>
        bool bChangeSeatMode;

        /// <summary>
        /// initializing commonly used items
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                // flight 
                Myflight = new clsFlightManager();

                // passenger
                MyPassenger = new clsPassengerManager();

                // flight combo box source
                cbChooseFlight.ItemsSource = Myflight.GetFlights();

                // add passenger window object
                wndAddPass = new wndAddPassenger();
                
                /// how the window behaves if our "modes" are in affect, disable everything but the seats              

                // DataSet ds = new DataSet();
                /* //Should probably not have SQL statements behind the UI
                 string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                 int iRet = 0;
                 clsData = new clsDataAccess();*/

                /* //This should probably be in a new class.  Would be nice if this new class
                //returned a list of Flight objects that was then bound to the combo box
                //Also should show the flight number and aircraft type together
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet); */

                //Should probably bind a list of flights to the combo box
                /* for(int i = 0; i < iRet; i++)
                 {
                     cbChooseFlight.Items.Add(ds.Tables[0].Rows[i][0]);
                 } */
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // string selection = cbChooseFlight.SelectedItem.ToString();  //This is wrong, if a list of flights was in the combo box, then could get the selected flight in an object

                /// establishing selected flight, enable passenger related data
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;


                // DataSet ds = new DataSet();                
                // int iRet = 0;

                //Should be using a flight object to get the flight ID here

                /// if the flight id of the selected flight is 1, a380 info is displayed, otherwise 767 is displayed
                if (clsSelectedFlight.sFlightID == "1")
                {
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Collapsed;
                    cbChoosePassenger.ItemsSource = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID);
                    FillPassengerSeats();
                }
                else
                {
                    Canvas767.Visibility = Visibility.Visible;
                    CanvasA380.Visibility = Visibility.Collapsed;
                    cbChoosePassenger.ItemsSource = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID);
                    FillPassengerSeats();
                }

                /* //I think this should be in a new class to hold SQL statments
                string sSQL = "SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                              "FROM Passenger, Flight_Passenger_Link FPL " +
                              "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                              "Flight_ID = " + cbChooseFlight.SelectedItem.ToString();//If the cbChooseFlight was bound to a list of Flights, the selected object would have the flight ID
                //Probably put in a new class
                ds = clsData.ExecuteSQLStatement(sSQL, ref iRet); */

                // cbChoosePassenger.Items.Clear();//Don't need if assigning a list of passengers to the combo box

                //Would be nice if code from another class executed the SQL above, added each passenger into a Passenger object,
                //then into a list of Passengers to be returned and bound to the combo box
                /* for (int i = 0; i < iRet; i++)
                {
                    cbChoosePassenger.Items.Add(ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                } */
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// directing the program to the add passenger page, if saved was successfully clicked, lock the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();

                 if(wndAddPassenger.Saved == true)
                {
                    bAddPassengerMode = true;
                    LockWindow();
                    ChangedSelection();
                } 

                
                //Check the Add Passenger Window to see if the user clicked Save and if they did,
                // then disable everything except the seats, so they are forced to click a seat
                //
                //Set the variable bAddPassengerMode that tells the user is in Add Passenger Mode
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// properly filling the passenger seats
        /// </summary>
        private void FillPassengerSeats()
        {
            try
            {
                ///current selected flight
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                
                if (clsSelectedFlight.sFlightID == "1")
                {
                    /// initially setting all seats back to empty
                    foreach (Label label in cA380_Seats.Children)
                    {
                        label.Background = Brushes.Blue;
                    }
                    ///looping through the passengers seat list, and then looping through the seat labels, if seat num matches passenger seat, mark as taken (red)
                    for (int i = 0; i < MyPassenger.GetPassengers(clsSelectedFlight.sFlightID).Count; i++)
                    {
                        foreach(Label label in cA380_Seats.Children)
                        {
                            if(label.Content.ToString() == MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].SeatNum)
                            {
                                label.Background = Brushes.Red;
                            }
                        }
                    }
                }
                else
                {
                    /// initially setting all seats back to empty
                    foreach (Label label in c767_Seats.Children)
                    {
                        label.Background = Brushes.Blue;
                    }

                    ///looping through the passengers seat list, and then looping through the seat labels, if seat num matches passenger seat, mark as taken (red)
                    for (int i = 0; i < MyPassenger.GetPassengers(clsSelectedFlight.sFlightID).Count; i++)
                    {
                        foreach (Label label in c767_Seats.Children)
                        {
                            if (label.Content.ToString() == MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].SeatNum)
                            {
                                label.Background = Brushes.Red;
                            }
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method will get called when a user clicks on any seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // button that is clicked
                var source = sender as Label;

                // selected flight
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;



                //What mode is in the program in? bAddPassengerMode or bChangeSeatMode or regular seat selection
                //
                //bAddPassengerMode
                // Insert a new passenger into the database, then insert a record  into the link table (Done in another class)
                //

                if (bAddPassengerMode == true)
                {
                    // if seat that is being clicked is red, give error, if not, make the seat red, add passenger to database
                    if (source.Background == Brushes.Red)
                    {
                        ErrorLabel.Content = "This Seat is already taken!";
                    }

                    else
                    {
                        source.Background = Brushes.Red;
                        bAddPassengerMode = false;
                        wndAddPassenger.Saved = false;
                        wndAddPass.ResetWindow();
                        MyPassenger.AddPassenger(wndAddPassenger.FirstName, wndAddPassenger.LastName);
                        MyPassenger.AddFlightLink(clsSelectedFlight.sFlightID, MyPassenger.GetPassengerID(wndAddPassenger.FirstName, wndAddPassenger.LastName), source.Content.ToString());
                        cbChoosePassenger.ItemsSource = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID);
                        UnlockWindow();
                        FillPassengerSeats();
                    }
                }

                else if (bChangeSeatMode == true)
                {
                    // if seat that is being clicked is red, give error, if not, make the seat red, update seat in database
                    if (source.Background == Brushes.Red)
                    {
                        ErrorLabel.Content = "This Seat is already taken!";
                    }

                    else
                    {
                        source.Background = Brushes.Red;
                        bChangeSeatMode = false;
                        for (int i = 0; i < MyPassenger.GetPassengers(clsSelectedFlight.sFlightID).Count; i++)
                        {
                            if ((MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].ToString() == cbChoosePassenger.SelectedItem.ToString()))
                            {
                                MyPassenger.UpdateSeat(MyPassenger.GetPassengerID(MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].FirstName, 
                                    MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].LastName), clsSelectedFlight.sFlightID, source.Content.ToString());


                            }
                        }        
                        cbChoosePassenger.ItemsSource = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID);
                        UnlockWindow();
                        FillPassengerSeats();
                    }
                }
                //bChangeSeatMode
                // Only Change the seat if the seat is empty
                // If it's empty then update the link table to update the user's new seat (done in another class)
                //

                //Regular Seat selection
                // If a seat is taken, loop through the passengers in the combobox
                // keep looping until the seat that was clicked, its number matches a passenger's seat number
                // then select that combo box index or selected item and put the passenger's seat in the label
                else
                {
                    if (source.Background == Brushes.Red)
                    {
                        ChangedSelection();
                        for (int i = 0; i < cbChoosePassenger.Items.Count; i++)
                        {

                            for (int j = 0; j < MyPassenger.GetPassengers(clsSelectedFlight.sFlightID).Count; j++)
                            {
                                if ((cbChoosePassenger.Items[i].ToString() == MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[j].ToString()))
                                {
                                    if (source.Content.ToString() == MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[j].SeatNum)
                                    {
                                        ErrorLabel.Content = "";
                                        source.Background = Brushes.Green;
                                        lblPassengersSeatNumber.Content = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].SeatNum;
                                        cbChoosePassenger.SelectedIndex = i;
                                    }


                                }
                            }



                        }
                    }

                    else if (source.Background == Brushes.Green)
                    {
                        ErrorLabel.Content = "Already Selected!";
                    }

                    else
                    {
                        ChangedSelection();
                        cbChoosePassenger.SelectedIndex = -1;
                        lblPassengersSeatNumber.Content = "";
                        ErrorLabel.Content = "No Passenger Selected";
                    }

                }
                
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Passenger is selected
                //Lock down window, set bChangeSeatMode to true, force user to select a seat

                if (cbChoosePassenger.SelectedItem != null)
                {
                    bChangeSeatMode = true;
                    LockWindow();
                }

                else
                {
                    ErrorLabel.Content = "Passenger Must Be Selected!";
                }
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Selected Passenger
                //Delete the currently selected passenger (Done in another class)
                // Reload the passengers into the combo box
                // Reload taken seats

                ///current selected flight
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                if (cbChoosePassenger.SelectedItem != null)
                {
                    for (int i = 0; i < MyPassenger.GetPassengers(clsSelectedFlight.sFlightID).Count; i++)
                    {
                        if ((MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].ToString() == cbChoosePassenger.SelectedItem.ToString()))
                        {
                            MyPassenger.DeleteFlightLink(clsSelectedFlight.sFlightID, MyPassenger.GetPassengerID(MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].FirstName,
                                MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].LastName));
                            MyPassenger.DeletePassenger(MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].PassengerID);
                            
                        }
                    }
                    cbChoosePassenger.ItemsSource = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID);
                    FillPassengerSeats();
                }

                else
                {
                    ErrorLabel.Content = "Passenger Must Be Selected!";
                }
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Locking the window
        /// </summary>
        public void LockWindow()
        {
            try
            {
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// unlocking the window after exiting out of modes
        /// </summary>
        public void UnlockWindow()
        {
            try
            {
                cbChooseFlight.IsEnabled = true;
                cbChoosePassenger.IsEnabled = true;
                cmdAddPassenger.IsEnabled = true;
                cmdChangeSeat.IsEnabled = true;
                cmdDeletePassenger.IsEnabled = true;
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// a method to set previous selection back to red when current selection is changed
        /// </summary>
        private void ChangedSelection()
        {
            try
            {
                // selected flight
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                // clearing the error label
                ErrorLabel.Content = "";

                if (clsSelectedFlight.sFlightID == "1")
                {
                    foreach (Label label in cA380_Seats.Children)
                    {
                        if (label.Background == Brushes.Green)
                        {
                            label.Background = Brushes.Red;
                        }
                    }
                }

                else
                {
                    foreach (Label label in c767_Seats.Children)
                    {
                        if (label.Background == Brushes.Green)
                        {
                            label.Background = Brushes.Red;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// how the window changes when a different passenger is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // selected flight
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                if (cbChoosePassenger.SelectedItem == null)
                {
                    lblPassengersSeatNumber.Content = "";
                }

                else
                {
                    ChangedSelection();
                    for (int i = 0; i < MyPassenger.GetPassengers(clsSelectedFlight.sFlightID).Count(); i++)
                    {
                        if (cbChoosePassenger.SelectedItem.ToString().Contains(MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].FirstName) &&
                            cbChoosePassenger.SelectedItem.ToString().Contains(MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].LastName))
                        {
                            lblPassengersSeatNumber.Content = MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].SeatNum;

                            if (clsSelectedFlight.sFlightID == "1")
                            {
                                foreach (Label label in cA380_Seats.Children)
                                {
                                    if (label.Content.ToString() == MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].SeatNum)
                                    {
                                        label.Background = Brushes.Green;
                                    }
                                }
                            }

                            else
                            {
                                foreach (Label label in c767_Seats.Children)
                                {
                                    if (label.Content.ToString() == MyPassenger.GetPassengers(clsSelectedFlight.sFlightID)[i].SeatNum)
                                    {
                                        label.Background = Brushes.Green;
                                    }
                                }
                            }
                        }
            
                    } 
                }
            }

            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
