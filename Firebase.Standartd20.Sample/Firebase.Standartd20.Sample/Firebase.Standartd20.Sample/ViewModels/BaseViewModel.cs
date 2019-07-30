using GalaSoft.MvvmLight.Command;

namespace Firebase.Standartd20.Sample.ViewModels
{
    public class BaseViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => Set<bool>(() => this.IsBusy, ref isBusy, value);
        }

        public RelayCommand OnLoadCommand => new RelayCommand(() => OnLoad());

        public virtual void OnLoad()
        {
        }
    }
}
