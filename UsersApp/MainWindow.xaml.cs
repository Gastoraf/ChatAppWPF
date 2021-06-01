using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
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

            MainFrame.Navigate(new Pages.StartPage());


        }

    }
}
