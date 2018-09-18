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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : UserControl
    {
        public BookingWindow()
        {
            InitializeComponent();
            userData.
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client.DataClient<Entities.HallBooking> dataClient = new Client.DataClient<Entities.HallBooking>();
            dataClient.AddData(dtpBookingDate.Value ?? DateTime.Now, tbxActivity.Text, );
        }
    }
}
