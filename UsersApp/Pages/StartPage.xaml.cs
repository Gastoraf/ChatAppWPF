using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
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


namespace UsersApp.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Button_Start_Texting(object sender, RoutedEventArgs e)
        {




            //ObjectCache cache = MemoryCache.Default;
            ////try reading the cache content first. if null then make an entry  
            //string fcontent = cache["filecontent"] as string;
            //if (string.IsNullOrEmpty(fcontent))
            //{
            //    //create an item policy for   
            //    CacheItemPolicy policy = new CacheItemPolicy();
            //    policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.0);
            //    List<string> filepath = new List<string>();
            //    filepath.Add("D:\\test.txt");
            //    policy.ChangeMonitors.Add(new HostFileChangeMonitor(filepath));
            //    fcontent = File.ReadAllText("D:\\test.txt") + "\n" + DateTime.Now;
            //    //adding items to cache with expiration policy and the object value  
            //    cache.Set("filecontent", fcontent, policy);
            //}
            
            //textBlockErrLogUp.Text = (string)App.Current.Properties["name"]; ;

            this.NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }
    }
}
