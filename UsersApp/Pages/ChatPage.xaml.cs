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

        string path = AppDomain.CurrentDomain.BaseDirectory + @"cloudfirestore.json";

        public ChatPage()
        {
            InitializeComponent();

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
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
            if (message != "")
            {
                database = FirestoreDb.Create("userapp-78315");

                CollectionReference coll = database.Collection("messages");
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                    {"nameUser", "sdasfasdas" },
                    {"text", message }
                };
                coll.AddAsync(data1);
            }

        }

        //Для получения сообщений из бд FireStore
        async void Get_All_Messages()
        {

            database = FirestoreDb.Create("userapp-78315");

            CollectionReference citiesRef = database.Collection("messages");
            Query query = database.Collection("messages").WhereEqualTo("nameUser", "sdasfasdas");

            FirestoreChangeListener listener = query.Listen(snapshot =>
            {
                string documentText = "";

                Console.WriteLine("get messages");
                foreach (DocumentSnapshot documentSnapshot in snapshot)
                {
                    Classes.Message message = documentSnapshot.ConvertTo<Classes.Message>();

                    documentText += "Name: " + message.nameUser + "\n";
                    documentText += "Text: " + message.text + "\n";
                }

                Dispatcher.Invoke(() => TextBlock1.Text = documentText);
            });

        }

    }
}
