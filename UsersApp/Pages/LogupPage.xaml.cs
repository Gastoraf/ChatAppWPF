using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for LogupPage.xaml
    /// </summary>
    public partial class LogupPage : Page
    {
        //configuring FireSharp
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "0cqByW3G1QU13uWhclmoVyxtvKvWmEgWnfU4GVq2",
            BasePath = "https://userapp-78315-default-rtdb.firebaseio.com/"
        };

        private readonly FirebaseClient client;

        public LogupPage()
        {
            InitializeComponent();

            client = new FireSharp.FirebaseClient(config);
        }

        // login verification
        private bool IsValidLogin(string login)
        {
            try
            {

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

        // email verification
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                if (addr.Address == email)
                {
                    textBoxEmail.ToolTip = null;
                    textBlockErrLogUp.Text = "";
                    textBoxEmail.Background = Brushes.Transparent;
                    return true;
                }
            }
            catch
            {
                textBoxEmail.ToolTip = "Invalid email";
                textBoxEmail.Background = Brushes.Red;
                textBlockErrLogUp.Text = "Invalid email. Please try again.";
                textBlockErrLogUp.Background = Brushes.Red;
                return false;
            }
            return false;
        }

        // password verification
        private bool IsValidPassword(string pass, string pass2)
        {
            try
            {
                if (pass.Length < 5)
                {
                    textBoxPass_1.ToolTip = "Password length is too short.";
                    textBoxPass_1.Background = Brushes.Red;
                    throw new Exception("Invalid password. Please try again.");

                }
                else if (!Regex.IsMatch(pass, "^[!-~0-9x]+$"))
                {
                    textBoxPass_1.ToolTip = "Unacceptable symbols.";
                    textBoxPass_1.Background = Brushes.Red;
                    throw new Exception("Invalid password. Please try again.");
                }
                else if (pass != pass2)
                {
                    textBoxPass_2.ToolTip = "Passwords do not match.";
                    textBoxPass_2.Background = Brushes.Red;
                    throw new Exception("Invalid password. Please try again.");
                }
                else
                {
                    textBlockErrLogUp.Text = "";
                    textBoxPass_1.ToolTip = null;
                    textBoxPass_1.Background = Brushes.Transparent;
                    textBoxPass_2.ToolTip = null;
                    textBoxPass_2.Background = Brushes.Transparent;
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

        private async void Button_LogUp_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLog.Text.Trim();
            string pass = textBoxPass_1.Password.Trim();
            string pass2 = textBoxPass_2.Password.Trim();
            string email = textBoxEmail.Text.Trim();

            if (IsValidLogin(login) && IsValidPassword(pass, pass2) && IsValidEmail(email))
            {
                User user = new User(login, email, pass);

                SetResponse response = await client.SetAsync("Users/" + login, user);
                User result = response.ResultAs<User>();

                if (result != null)
                {
                    textBlockErrLogUp.Text = "You are registered!";
                    textBlockErrLogUp.Background = Brushes.LightGreen;
                }

            }

        }

        private void Button_Click_Navigation_LoginPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }
    }
}
