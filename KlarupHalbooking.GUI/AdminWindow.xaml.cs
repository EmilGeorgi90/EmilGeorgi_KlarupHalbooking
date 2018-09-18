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

namespace KlarupHalbooking.GUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : UserControl
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Client.DataClient dataClient = new Client.DataClient();
            List<Entities.HallBooking> bookings = new List<Entities.HallBooking>();
            foreach (Entities.HallBooking booking in dataClient.GetData())
            {
                bookings.Add(booking);
            }
            dgBookings.ItemsSource = bookings;
        }
    }
}
