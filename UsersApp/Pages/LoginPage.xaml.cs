using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        #region Configuring FireSharp
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "0cqByW3G1QU13uWhclmoVyxtvKvWmEgWnfU4GVq2",
            BasePath = "https://userapp-78315-default-rtdb.firebaseio.com/"
        };
        #endregion

        private readonly FirebaseClient client;

        public LoginPage()
        {
            InitializeComponent();

            client = new FireSharp.FirebaseClient(config);

            var cache = new MemoryCache("demoCache");

            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1.0)
            };

            cache.Set("fullName", 1, cacheItemPolicy);

            var result = cache.GetCacheItem("fullName");
            Console.WriteLine("fullName {0}", result.Value);

        }

        #region Login Verification
        private async Task<bool> IsValidLoginAsync(string login)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync("Users/" + login);
                Classes.User obj = response.ResultAs<Classes.User>();
                if (login.Length < 5)
                {
                    textBoxLog.ToolTip = "Login length is too short.";
                    textBoxLog.Background = Brushes.Red;
                    throw new Exception("Invalid login. Please try again.");
                }
                else if (!Regex.IsMatch(login, "^[a-z0-9x]+$"))
                {
                    textBoxLog.ToolTip = "Unacceptable symbols.";
                    textBoxLog.Background = Brushes.Red;
                    throw new Exception("Invalid login. Please try again.");
                }
                else if (obj == null)
                {
                    textBoxLog.ToolTip = "There is no user with this login.";
                    textBoxLog.Background = Brushes.Red;
                    throw new Exception("There is no user with this login.");
                }
                else
                {
                    textBoxLog.ToolTip = null;
                    textBlockErrLogUp.Text = "";
                    textBoxLog.Background = Brushes.Transparent;
                    return true;
                }

            }
            catch (Exception ex)
            {
                textBlockErrLogUp.Text = ex.Message;
                textBlockErrLogUp.Background = Brushes.Red;
                return false;
            }

        }
        #endregion

        #region Password Verification
        private async Task<bool> IsValidPasswordAsync(string pass, string login)
        {
            try
            {
                FirebaseResponse response = await client.GetAsync("Users/" + login);
                Classes.User obj = response.ResultAs<Classes.User>();
                if (pass.Length < 5)
                {
                    textBoxPass.ToolTip = "Password length is too short.";
                    textBoxPass.Background = Brushes.Red;
                    throw new Exception("Invalid password. Please try again.");

                }
                else if (!Regex.IsMatch(pass, "^[!-~0-9x]+$"))
                {
                    textBoxPass.ToolTip = "Unacceptable symbols.";
                    textBoxPass.Background = Brushes.Red;
                    throw new Exception("Invalid password. Please try again.");
                }
                else if (pass != obj.Password)
                {
                    textBoxPass.ToolTip = "The password was entered incorrectly.";
                    textBoxPass.Background = Brushes.Red;
                    throw new Exception("The password was entered incorrectly.");
                }
                else
                {
                    textBlockErrLogUp.Text = "";
                    textBoxPass.ToolTip = null;
                    textBoxPass.Background = Brushes.Transparent;
                    return true;
                }
            }
            catch (Exception ex)
            {
                textBlockErrLogUp.Text = ex.Message;
                textBlockErrLogUp.Background = Brushes.Red;
                return false;
            }

        }
        #endregion
        #region Login
        private async void Button_LogIn_ClickAsync(object sender, RoutedEventArgs e)
        {
            string login = textBoxLog.Text.Trim();
            string pass = textBoxPass.Password.Trim();

            if (await IsValidLoginAsync(login) && await IsValidPasswordAsync(pass, login))
            {
                FirebaseResponse response = await client.GetAsync("Users/" + login);
                Classes.User obj = response.ResultAs<Classes.User>();

                //Сохраняем логин для дальнейшего использования
                App.Current.Properties.Add("name", obj.Login);
                
                if (obj.Login != "")
                {
                    this.NavigationService.Navigate(new Uri("Pages/ChatMenuPage.xaml", UriKind.Relative));
                }

            }
        }
        #endregion

        private void Button_Click_Navigation_LogupPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/LogupPage.xaml", UriKind.Relative));
        }
    }
}
