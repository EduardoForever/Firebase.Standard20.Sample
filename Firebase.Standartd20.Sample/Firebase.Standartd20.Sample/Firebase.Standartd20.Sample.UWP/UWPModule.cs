using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.Standartd20.Sample.UWP
{
    public class UWPModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<FirebaseAppNameResolver>().As<IFirebaseAppNameResolver>().SingleInstance();
        }
    }
}
