using Firebase.Standartd20.Sample.Config;
using Firebase.Standartd20.Sample.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Standartd20.Sample.Services.Base
{
    public class FirebaseService<T> where T : FirebaseModel
    {
        private readonly CollectionReference collection;
        private readonly IFirebaseAuthService firebaseAuthService;

        public FirebaseService(IFirebaseAuthService firebaseAuthService, string collectionName)
        {
            FirestoreDb db = FirestoreDb.Create(ApiKeys.FirebaseProjectId, firebaseAuthService.Client);

            collection = db.Collection(collectionName);
            this.firebaseAuthService = firebaseAuthService;
        }

        public Task AddItem(T item)
        {
            return collection.AddAsync(item);
        }

        public Task DeleteItem(T item)
        {
            return collection.Document(item.Id).DeleteAsync();
        }

        public async Task<List<T>> GetItemsAsync()
        {
            var resultList = new List<T>();
            QuerySnapshot allItems = await collection.GetSnapshotAsync();

            foreach (DocumentSnapshot document in allItems.Documents)
            {
                // Do anything you'd normally do with a DocumentSnapshot
                var item = document.ConvertTo<T>();

                resultList.Add(item);
            }

            return resultList;
        }
    }
}
