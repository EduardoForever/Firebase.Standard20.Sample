using Firebase.Standartd20.Sample;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Firebase.Standartd20.Sample.Services.Implementation
{
    public class NavigationService
    {
        public Task PushAsync(Page page) => GetCurrentNavigation().PushAsync(page);
        public Task PopAsync() => GetCurrentNavigation().PopAsync();

        INavigation GetCurrentNavigation() => (Application.Current as App).MainPage.Navigation;

    }
}
