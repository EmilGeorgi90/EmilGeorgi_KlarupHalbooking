using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal bool isLoggedIn = false;
        internal bool isAdmin = false;
        internal Entities.UserData userData = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Entities.UserData userData)
        {
            this.userData = userData;
            InitializeComponent();
        }

        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isLoggedIn)
            {
                Content = new LoginWindow();
            }
            else if(!isAdmin)
            {
                Content = new BookingWindow();
            }
            else
            {
                Content = new AdminWindow();
            }
        }
    }
}
