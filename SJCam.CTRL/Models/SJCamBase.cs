

namespace SJCam.CTRL.Models
{
    abstract public class SJCamBase : BindableBase
    {
        private bool _IsConnected = false;
        public string Url { get; private set; }
        private int _PhotosLeft = 0;
        private int _VideoRecordingLeft = 0;
        private bool _IsVideoRecording = false;
        private CameraMode _Mode;

        public CameraMode CameraMode
        {
            get { return _Mode; }
            set { SetProperty(ref _Mode, value); }
        }
        public bool IsConnected
        {
            get { return _IsConnected; }
            set { SetProperty(ref _IsConnected, value); }
        }


        public int PhotosLeft
        {
            get { return _PhotosLeft; }
            set { SetProperty(ref _PhotosLeft, value); }
        }
        public int VideoRecordingLeft
        {
            get { return _VideoRecordingLeft; }
            set { SetProperty(ref _VideoRecordingLeft, value); }
        }

        public bool IsVideoRecording
        {
            get { return _IsVideoRecording; }
            set { SetProperty(ref _IsVideoRecording, value); }
        }


        public abstract int PhotosLeftCommand { get; }
        public abstract int VideoRecordingLeftCommand { get; }
        public abstract int ModeCommand { get; }
        public abstract int SnapPhotoCommand { get; }
        public abstract int VideoCommand { get; }
        public abstract int VideoRecordingStatusCommand { get; }


        public int StartVideoParameter { get { return 1; } }
        public int StopVideoParameter { get { return 0; } }

        public SJCamBase(string url)
        {
            Url = url;
        }
    }
}
