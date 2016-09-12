using SJCam.CTRL.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SJCam.CTRL.ViewModels
{
    public class SJCamViewModel : BindableBase
    {
        Models.SJCam _Camera;
        ICommand _SetModeToPhotoCommand;
        ICommand _SetModeToVideoCommand;

        ICommand _RefreshCommand;
        ICommand _ActionCommand;
        private readonly SJCamRefresher _Refresher;

        private HttpClient _httpClient;


        public Models.SJCam Camera
        {
            get { return _Camera; }
            set { SetProperty(ref _Camera, value); }
        }
        public ICommand ActionCommand
        {
            get { return _ActionCommand; }
            set { SetProperty(ref _ActionCommand, value); }
        }

        public ICommand RefreshCommand
        {
            get { return _RefreshCommand; }
            set { SetProperty(ref _RefreshCommand, value); }
        }

        public ICommand SetModeToPhotoCommand
        {
            get { return _SetModeToPhotoCommand; }
            set { SetProperty(ref _SetModeToPhotoCommand, value); }
        }
        public ICommand SetModeToVideoCommand
        {
            get { return _SetModeToVideoCommand; }
            set { SetProperty(ref _SetModeToVideoCommand, value); }
        }

        public SJCamViewModel()
        {

            Camera = new Models.SJCam();

            _Refresher = new SJCamRefresher(Camera);

            _httpClient = new HttpClient();

            _httpClient = new HttpClient(new HttpClientHandler());

            RefreshCommand = new RelayCommand((x) =>
            {
                _Refresher.Refresh();
            }, (x) => { return true; });

            ActionCommand = new RelayCommand((x) =>
            {
                Snap();
            }, (x) => { return true; });

            _SetModeToPhotoCommand = new RelayCommand((x) =>
            {
                ChangeMode(CameraMode.Photo);
            }, (x) => { return true;/* _Camera.CameraMode != CameraMode.Photo; */});

            _SetModeToVideoCommand = new RelayCommand((x) =>
            {
                ChangeMode(CameraMode.Video);
            }, (x) => { return true; /*_Camera.CameraMode != CameraMode.Video;*/ });
        }


        public async void Snap()
        {
            switch (_Camera.CameraMode)
            {
                case CameraMode.Unknown:
                    _Refresher.Refresh();
                    break;
                case CameraMode.Photo:
                    await TakePhoto();
                    break;
                case CameraMode.Video:
                    await ToggleVideo();
                    break;
                default:
                    break;
            }

            _Refresher.Refresh();
        }

        public async Task<CameraMode> ChangeMode(CameraMode mode)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_Camera.Url}?custom=1&cmd={_Camera.ModeCommand}&par={(int)mode}");
            _Refresher.Refresh();

            if (response.IsSuccessStatusCode)
            {
                Camera.CameraMode = mode;
                return Camera.CameraMode;
            }


            return CameraMode.Unknown;
        }

        public async Task<CameraMode> TakePhoto()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_Camera.Url}?custom=1&cmd={_Camera.SnapPhotoCommand}");
            _Refresher.Refresh();

            if (response.IsSuccessStatusCode)
            {
                return Camera.CameraMode;
            }


            return CameraMode.Unknown;
        }
        public async Task<CameraMode> ToggleVideo()
        {
            int commandParameter = _Camera.IsVideoRecording ? _Camera.StopVideoParameter : _Camera.StartVideoParameter;
            HttpResponseMessage response = await _httpClient.GetAsync($"{_Camera.Url}?custom=1&cmd={_Camera.VideoCommand}&par={commandParameter}");
            _Refresher.Refresh();

            if (response.IsSuccessStatusCode)
            {
                return Camera.CameraMode;
            }


            return CameraMode.Unknown;
        }

    }
}
