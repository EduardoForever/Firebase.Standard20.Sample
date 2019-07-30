using Google.Cloud.Firestore;
using System;
namespace Firebase.Standartd20.Sample.Models
{
    [FirestoreData]
    public class Place : FirebaseModel
    {
        [FirestoreProperty]
        public string PlaceName { get; set; }

        [FirestoreProperty]
        public string PlaceImage { get; set; }

        [FirestoreProperty]
        public string Description { get; set; }
    }
}
