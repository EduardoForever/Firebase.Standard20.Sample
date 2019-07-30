using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Standartd20.Sample.Services
{
    public interface IFirebaseAppNameResolver
    {
        string FirebaseAppName { get; }
    }
}
