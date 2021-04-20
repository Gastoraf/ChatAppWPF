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
using Google.Cloud.Firestore;

namespace UsersApp.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        /// <summary>
        /// Для работы с бд firebase
        /// </summary>
        FirestoreDb database;

        public ChatPage()
        {
            InitializeComponent();

            string path = AppDomain.CurrentDomain.BaseDirectory + @"cloudfirestore.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("userapp-78315");
        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
            
            string message = textBoxMessage.Text.Trim();
            Add_message(message);
            textBoxMessage.Text = "";
            Get_All_Messages();
        }

        void Add_message(string message)
        {
            if (message != "") {
                CollectionReference coll = database.Collection("messages");
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                    {"nameUser", "sdasfasdas" },
                    {"text", message }
                };
                coll.AddAsync(data1);
                //MessageBox.Show("Ura");
            }
             
        }

        //Для получения сообщений из бд FireStore
        async void Get_All_Messages()
        {
            Query Qref = database.Collection("messages");
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            TextBlock1.Text = "";

            foreach (DocumentSnapshot docsnap in snap)
            {
                Classes.Message message = docsnap.ConvertTo<Classes.Message>();
                if (docsnap.Exists)
                {
                    TextBlock1.Text += "User:" + message.nameUser + "\n";
                    TextBlock1.Text += "Text:" + message.text + "\n\n";

                }
            }

        }

    }
}
