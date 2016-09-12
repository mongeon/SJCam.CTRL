

namespace SJCam.CTRL.Models
{
    public class SJCam : SJCamBase
    {
        public SJCam():base("http://192.168.1.254/")
        {

        }

        public override int PhotosLeftCommand
        {
            get
            {
                return 1003;
            }
        }

        public override int VideoRecordingLeftCommand
        {
            get
            {
                return 2009;
            }
        }
        public override int ModeCommand
        {
            get
            {
                return 3001;
            }
        }

        public override int SnapPhotoCommand
        {
            get
            {
                return 1001;
            }
        }

        public override int VideoCommand
        {
            get
            {
                return 2001;
            }
        }

        public override int VideoRecordingStatusCommand
        {
            get
            {
                return 2016;
            }
        }
    }
}
