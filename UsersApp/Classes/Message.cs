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
        public string nameUser { get; set; }
        [FirestoreProperty]
        public string text { get; set; }
    }
}
