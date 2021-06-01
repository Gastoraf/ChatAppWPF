using Google.Cloud.Firestore;
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

namespace UsersApp.Pages
{
    /// <summary>
    /// Interaction logic for ChatMenuPage.xaml
    /// </summary>
    public partial class ChatMenuPage : Page
    {
        /// <summary>
        /// Для работы с бд firebase
        /// </summary>
        FirestoreDb database;

        string path = AppDomain.CurrentDomain.BaseDirectory + @"cloudfirestore.json";
        public ChatMenuPage()
        {
            InitializeComponent();

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            nameOfUser.Text = (string)App.Current.Properties["name"];
            nameOfUser1.Text = (string)App.Current.Properties["name"];

            Get_All_Messages();
        }

        private void BtnSendMessage(object sender, RoutedEventArgs e)
        {
            string message = textBoxMessage.Text.Trim();
            Add_message(message);
            textBoxMessage.Text = "";

        }

        //Добавляет сообщение в бд
        void Add_message(string message)
        {
            if (message != "")
            {
                database = FirestoreDb.Create("userapp-78315");

                var date1 = DateTime.Now;

                var unspecified = new DateTime(2016, 12, 12, 10, 10, 10, DateTimeKind.Unspecified);
                var specified = DateTime.SpecifyKind(date1, DateTimeKind.Utc);

                var myTimestamp = Timestamp.FromDateTime(specified);


                CollectionReference coll = database.Collection("messages");
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                    {"nameUser", (string)App.Current.Properties["name"]},
                    {"text", message },
                    {"PublishDate",  myTimestamp}
                };
                coll.AddAsync(data1);
            }

        }

        //Для получения сообщений из бд FireStore
        async void Get_All_Messages()
        {

            database = FirestoreDb.Create("userapp-78315");

            CollectionReference citiesRef = database.Collection("messages");
            Query query = citiesRef.OrderBy("PublishDate");

            FirestoreChangeListener listener = query.Listen(snapshot =>
            {

                List<Classes.Message> messages = new List<Classes.Message>();

                Console.WriteLine("get messages");
                foreach (DocumentSnapshot documentSnapshot in snapshot)
                {
                    Classes.Message message = documentSnapshot.ConvertTo<Classes.Message>();
                    message.Usernow = (string)App.Current.Properties["name"];
                    if (message.nameUser == message.Usernow)
                    {
                        message.Usernow = "Yes";
                    } else
                    {
                        message.Usernow = "No";
                    }
                    
                    messages.Add(message);

                }
                Dispatcher.Invoke(() => messagesList.ItemsSource = messages);
            });

        }
    }
}
