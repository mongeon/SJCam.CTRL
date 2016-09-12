using System;

namespace SJCamCTRL.Models
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
    }
}
