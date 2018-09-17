﻿using System;
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

namespace KlarupHalbooking.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isLoggedIn = false;
        public MainWindow()
        {
            InitializeComponent();
            if (!isLoggedIn)
            {
                Content = new LoginWindow();
            }
        }

        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
