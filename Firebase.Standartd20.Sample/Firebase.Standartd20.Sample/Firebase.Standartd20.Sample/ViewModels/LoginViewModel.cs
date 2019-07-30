using Firebase.Standartd20.Sample.Services;
using Firebase.Standartd20.Sample.Services.Implementation;
using Firebase.Standartd20.Sample.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace Firebase.Standartd20.Sample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly IFirebaseAuthService firebaseAuthService;
        readonly NavigationService navigationService;

        private string email;
        private string password;

        public String Email
        {
            get => this.email;
            set => Set<string>(() => this.Email, ref this.email, value);
        }

        public String Password
        {
            get => this.password;
            set => Set<string>(() => this.Password, ref this.password, value);
        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand SpeachCommand { get; set; }

        public LoginViewModel(
            IFirebaseAuthService firebaseAuthService,
            NavigationService navigationService)
        {
            this.firebaseAuthService = firebaseAuthService;
            this.navigationService = navigationService;

            this.LoginCommand = new RelayCommand(async () => await Login());
            this.SpeachCommand = new RelayCommand(async () => await Speach());

            this.Email = "porchinskiy@gmail.com";
            this.Password = "123456";
        }

        async Task Login()
        {
            this.IsBusy = true;

            try
            {
                var isSuccess = await this.firebaseAuthService.LoginAsync(this.Email, this.Password);

                if (isSuccess)
                {
                    await this.navigationService.PushAsync(new MainPage());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            this.IsBusy = false;
        }

        async Task Speach()
        {

            var locales = await TextToSpeech.GetLocalesAsync();

            var settings = new SpeechOptions()
            {
                Volume = .75f,
                Pitch = 1.0f,
                Locale = locales.ToArray()[2]
            };
            await TextToSpeech.SpeakAsync("Privet", settings);
        }
    }
}
