using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Firebase.Standartd20.Sample;
using Firebase.Standartd20.Sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Firebase.Standartd20.Sample.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            this.viewModel = (Application.Current as App).Container.Resolve<MainViewModel>();

            this.BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.viewModel.OnLoadCommand.Execute(null);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.viewModel.OpenPlaceCommand.Execute(e.Item);
        }
    }
}