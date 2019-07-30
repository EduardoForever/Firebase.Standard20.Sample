using Firebase.Standartd20.Sample.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Cloud.Firestore.V1;
using Grpc.Core;
using Grpc.Auth;

namespace Firebase.Standartd20.Sample.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public FirestoreClient Client { get; protected set; }

        public async Task<bool> LoginAsync(string email, string password)
        {
            GoogleCredential credential = GoogleCredential
            .FromFile("auth.json");
            ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            FirestoreClient firestoreClient = FirestoreClient.Create(channel);

            this.Client = firestoreClient;

            return await Task.FromResult(true);
            //try
            //{
            //    UserCredential credential;
            //    using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            //    {
            //        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            //            GoogleClientSecrets.Load(stream).Secrets,
            //            new[] { BooksService.Scope.Books },
            //            "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
            //    }

            //    //this._authResult = await CrossFirebaseAuth.Current
            //    //        .GetInstance(ApiKeys.FirebaseName)
            //    //        .SignInWithEmailAndPasswordAsync(email, password);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}


            ////return this._authResult != null && this._authResult.User != null;

            //return await Task.FromResult(false);
        }
    }
}
