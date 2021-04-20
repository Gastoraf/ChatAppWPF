using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace UsersApp
{

    public partial class MainWindow : Window
    {
        ///
        public MainWindow()
        {
            //this.DataContext = new WindowsViewModel(this);

            InitializeComponent();

            MainFrame.Navigate(new Pages.ChatPage());


        }

        //private void Btn1(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Pages.LoginPage();
        //}
        //private void Btn2(object sender, RoutedEventArgs e)
        //{
        //    Main.Content = new Pages.LogupPage();
        //}

    }
}
