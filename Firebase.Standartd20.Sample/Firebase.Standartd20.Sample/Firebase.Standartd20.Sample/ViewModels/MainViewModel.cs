using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Firebase.Standartd20.Sample.Models;
using Firebase.Standartd20.Sample.Services;
using Firebase.Standartd20.Sample.Services.Implementation;
using Firebase.Standartd20.Sample.Views;
using GalaSoft.MvvmLight.Command;

namespace Firebase.Standartd20.Sample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly NavigationService navigationService;
        private readonly MessageService messageService;
        private IDataStore<Place> placesSerive;

        private List<Place> places;

        public MainViewModel(
            IDataStore<Place> placesSerive,
            NavigationService navigationService,
            MessageService messageService)
        {
            this.placesSerive = placesSerive;
            this.navigationService = navigationService;
            this.messageService = messageService;
            this.AddPlaceCommand = new RelayCommand(() => this.DoAddPlace());
            this.OpenPlaceCommand = new RelayCommand<Place>((place) => this.DoOpenPlace(place));
        }

        public List<Place> Places
        {
            get => places;
            set => Set<List<Place>>(() => this.Places, ref places, value);
        }

        public RelayCommand AddPlaceCommand { get; set; }

        public RelayCommand<Place> OpenPlaceCommand { get; set; }


        public override void OnLoad()
        {
            base.OnLoad();

            this.LoadPlaces();
        }

        private async void LoadPlaces()
        {
            try
            {
                this.Places = await this.placesSerive.GetItemsAsync();
            }
            catch (Exception ex)
            {
                messageService.ShowDialog("Error", ex.Message, "Ok");
            }
        }

        private void DoAddPlace()
        {
            this.navigationService.PushAsync(new AddPlacePage());
        }

        private void DoOpenPlace(Place place)
        {
            this.navigationService.PushAsync(new PlaceDetailsPage(place));
        }
    }
}
