using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace UsersApp.Classes
{
    /// <summary>
    /// Для роботы с сообщениями из FireStore
    /// </summary>
    [FirestoreData]
    public class Message
    {
        [FirestoreProperty]
        public DateTime PublishDate { get; set; }

        [FirestoreProperty]
        public string nameUser { get; set; }

        [FirestoreProperty]
        public string text { get; set; }

        public string Usernow { get; set; }

        //public Message(string usernow)
        //{
            
        //    Usernow = (string)App.Current.Properties["name"];
        //    if (Usernow == nameUser)
        //    {
        //        Usernow = "Yes";
        //    }

        //}

    }
}
