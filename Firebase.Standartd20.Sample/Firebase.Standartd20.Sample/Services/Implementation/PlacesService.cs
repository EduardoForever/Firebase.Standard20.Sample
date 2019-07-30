using Firebase.Standartd20.Sample.Models;
using Firebase.Standartd20.Sample.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Standartd20.Sample.Services.Implementation
{
    public class PlacesService : FirebaseService<Place>
    {
        public PlacesService(IFirebaseAuthService firebaseAuthService, string path)
            : base(firebaseAuthService, path)
        {
        }
    }
}
