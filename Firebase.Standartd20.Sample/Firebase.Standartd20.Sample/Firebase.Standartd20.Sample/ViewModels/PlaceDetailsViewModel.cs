using Firebase.Standartd20.Sample.Models;
using Firebase.Standartd20.Sample.Services;
using Firebase.Standartd20.Sample.Services.Implementation;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Standartd20.Sample.ViewModels
{
    public class PlaceDetailsViewModel : BaseViewModel
    {
        private readonly IDataStore<Place> placesSerive;

        readonly NavigationService navigationService;
        readonly MessageService messageService;

        private Place place;

        public Place Place
        {
            get => this.place;
            set => Set<Place>(() => this.Place, ref this.place, value);
        }

        public RelayCommand RemovePlaceCommand { get; set; }

        public PlaceDetailsViewModel(
            IDataStore<Place> placesSerive,
            NavigationService navigationService,
            MessageService messageService)
        {
            this.placesSerive = placesSerive;
            this.navigationService = navigationService;
            this.messageService = messageService;
            
            this.RemovePlaceCommand = new RelayCommand(() => this.DoRemovePlace());
        }
        private async void DoRemovePlace()
        {
            this.IsBusy = true;

            try
            {
                await this.placesSerive.DeleteItem(this.Place);

                await this.navigationService.PopAsync();
            }
            catch
            {
                await this.messageService.ShowDialog("Error", "Something going wrong", "Ok");
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
