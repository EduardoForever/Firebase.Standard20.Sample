using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Standartd20.Sample.Models
{
    [FirestoreData]
    public class FirebaseModel
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string OwnerId { get; set; }
    }
}
