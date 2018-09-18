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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : UserControl
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Client.DataClient<Entities.UserData> client = new Client.DataClient<Entities.UserData>();
            if (client.Login(new Entities.UserData(tbxUsernameInput.Text, tbxPasswordInput.Password, "00000000")))
            {
                MainWindow mainWindow = new MainWindow(new Entities.UserData(tbxUsernameInput.Text, tbxPasswordInput.Password, "00000000"))
                {
                    isLoggedIn = true
                };
                mainWindow.Show();
                Application.Current.MainWindow.Close();
            }
        }
    }
}
