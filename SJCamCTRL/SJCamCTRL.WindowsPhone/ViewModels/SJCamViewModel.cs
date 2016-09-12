using System.Windows.Input;

namespace SJCamCTRL.ViewModels
{
    public class SJCamViewModel : BindableBase
    {
        Models.SJCam _Camera;
        ICommand _RefreshCommand;
        private readonly SJCamRefresher _Refresher;

        public Models.SJCam Camera
        {
            get { return _Camera; }
            set { SetProperty(ref _Camera, value); }
        }

        public ICommand RefreshCommand
        {
            get { return _RefreshCommand; }
            set { SetProperty(ref _RefreshCommand, value); }
        }

        public SJCamViewModel()
        {

            Camera = new Models.SJCam();

            _Refresher = new SJCamRefresher(Camera);

            RefreshCommand = new RelayCommand((x) =>
            {
                _Refresher.Snap();
            }, (x) => { return true; });
        }

    }
}
