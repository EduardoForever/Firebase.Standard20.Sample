using Autofac;
using Firebase.Standartd20.Sample;
using Firebase.Standartd20.Sample.Services;
using Xamarin.Forms;

namespace Firebase.Standartd20.Sample.Config
{
    public static class ApiKeys
    {
        public static string FirebaseName
        {
            get
            {
                var container = (Application.Current as App).Container.BeginLifetimeScope();

                using (var scope = container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<IFirebaseAppNameResolver>();

                    return service.FirebaseAppName;
                }
            }
        }

        public const string FirebaseApiKey = "AIzaSyBcBd6S2QVcBYk3L0wvN0ZSBaQ3j-IbOp8";
        //public const string FirebaseApiKey = "AIzaSyASsPCCqrcMSlQcKEN1eHnQiwutIB5OiA4";
        public const string FirebaseUrl = "https://fir-sample-c6cc7.firebaseio.com/";
        public const string FirebasePath = "places";
        public const string FirebaseProjectId = "fir-sample-c6cc7";
    }
}
