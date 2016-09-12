using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJCamCTRL.Models
{
    abstract public class SJCamBase : BindableBase
    {
        private bool _IsConnected = false;
        public string Url { get; private set; }
        private int _PhotosLeft = 0;
        private int _VideoRecordingLeft = 0;

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


        public abstract int PhotosLeftCommand { get; }
        public abstract int VideoRecordingLeftCommand { get; }

        public SJCamBase(string url)
        {
            Url = url;
        }
    }
}
