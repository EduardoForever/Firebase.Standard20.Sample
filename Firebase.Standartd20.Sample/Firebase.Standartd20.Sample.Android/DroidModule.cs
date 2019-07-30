using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

namespace Firebase.Standartd20.Sample.Droid
{
    public class DroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<FirebaseAppNameResolver>().As<IFirebaseAppNameResolver>().SingleInstance();
        }
    }
}