using System;
using System.Collections.Generic;
using System.Linq;
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

namespace KlarupHalBooking.GUI
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : UserControl
    {
        Client.DataClient dataClient = null;
        public BookingWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        if (dtpBookingDate.Value < DateTime.Now)
                        {
                            MessageBox.Show("du kan ikke lave en booking der er mindre end dags dato");
                        }
                        else
                        {
                            if (dataClient.AddData(dtpBookingDate.Value ?? DateTime.Now, dtpBookingEndDate.Value ?? DateTime.Now, (string)cmbActivity.SelectedItem, (window as MainWindow).userData))
                            {
                                MessageBox.Show("du har nu tilføjet en booking, den bliver nu kigget på af administratorerne");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " contact your supporter");
                Application.Current.Shutdown();
            }
        }

        private void cmbActivity_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dataClient = new Client.DataClient();
                List<string> activityNames = new List<string>();
                foreach (Entities.Activity activity in dataClient.GetActivities())
                {
                    activityNames.Add(activity.ActivityName);
                }
                cmbActivity.ItemsSource = activityNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " contact your supporter");
                Application.Current.Shutdown();
            }
        }
    }
}
