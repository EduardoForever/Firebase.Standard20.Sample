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
using System.Reflection;
using System.Linq;
using Firebase.Auth;
using Google.Cloud.Firestore;
using Firebase.Standartd20.Sample.Models;

namespace Firebase.Standartd20.Sample.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private FirebaseAuthLink auth;

        public FirestoreClient Client { get; protected set; }

        public async Task<bool> LoginAsync(string email, string password)
        {
            // Create a custom authentication mechanism for Email/Password authentication
            // If the authentication is successful, we will get back the current authentication token and the refresh token
            // The authentication expires every hour, so we need to use the obtained refresh token to obtain a new authentication token as the previous one expires
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Config.ApiKeys.FirebaseApiKey));
            

            var callCredentials = CallCredentials.FromInterceptor(async (context, metadata) =>
            {
                if (auth.IsExpired()) auth = await auth.GetFreshAuthAsync();
                if (string.IsNullOrEmpty(auth.FirebaseToken)) return;

                metadata.Clear();
                metadata.Add("authorization", $"Bearer {auth.FirebaseToken}");
            });
            var credentials = ChannelCredentials.Create(new SslCredentials(), callCredentials);

            // Create a custom Firestore Client using custom credentials
            var channelOptions = new List<ChannelOption>();
            channelOptions.Add(new ChannelOption("GRPC_VERBOSITY", "DEBUG"));
            channelOptions.Add(new ChannelOption("GRPC_DNS_RESOLVER", "native"));
            channelOptions.Add(new ChannelOption("GRPC_TRACE", "api,cares_resolver,cares_address_sorting"));

            var grpcChannel = new Channel("firestore.googleapis.com",  credentials, channelOptions);
            var grcpClient = new Firestore.FirestoreClient(grpcChannel);

            var settings = FirestoreSettings.GetDefault();
            var firestoreClient = new FirestoreClientImpl(grcpClient, settings);

            //GoogleCredential credential = GoogleCredential
            //.FromJson(GetCredentialsJson());
            //ChannelCredentials channelCredentials = credential.ToChannelCredentials();
            //Channel channel = new Channel(FirestoreClient.DefaultEndpoint.ToString(), channelCredentials);
            //FirestoreClient firestoreClient = FirestoreClient.Create(channel);

            this.Client = firestoreClient;

            var firestoreDb = FirestoreDb.Create(Config.ApiKeys.FirebaseProjectId, firestoreClient);

            this.auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            var allItems = await firestoreDb.Collection(ApiKeys.FirebasePath).GetSnapshotAsync();

            var resultList = new List<Place>();

            foreach (DocumentSnapshot document in allItems.Documents)
            {
                // Do anything you'd normally do with a DocumentSnapshot
                var item = document.ConvertTo<Place>();

                resultList.Add(item);
            }

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

        private String GetCredentialsJson()
        {
            var fileName = "credentials.json";
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);
            var json = String.Empty;

            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }
    }
}
