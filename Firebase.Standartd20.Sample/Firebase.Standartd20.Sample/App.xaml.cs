using Autofac;
using Firebase.Standartd20.Sample.Config;
using Firebase.Standartd20.Sample.Models;
using Firebase.Standartd20.Sample.Services;
using Firebase.Standartd20.Sample.Services.Implementation;
using Firebase.Standartd20.Sample.ViewModels;
using Firebase.Standartd20.Sample.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firebase.Standartd20.Sample
{
    public partial class App : Application
    {
        public IContainer Container { get; }

        public App(Module module)
        {
            InitializeComponent();

            Container = BuildContainer(module);
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        IContainer BuildContainer(Module module)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<AddPlaceViewModel>().AsSelf();
            builder.RegisterType<PlaceDetailsViewModel>().AsSelf();
            builder.RegisterType<NavigationService>().AsSelf().SingleInstance();
            builder.RegisterType<MessageService>().AsSelf().SingleInstance();
            builder.RegisterType<FirebaseAuthService>().As<IFirebaseAuthService>().SingleInstance();
            builder.Register(componentContext =>
            {
                var firebaseAuthService = componentContext.Resolve<IFirebaseAuthService>();

                return new PlacesService(firebaseAuthService, ApiKeys.FirebasePath);
            }).As<PlacesService>();

            builder.RegisterModule(module);

            return builder.Build();
        }
    }
}
