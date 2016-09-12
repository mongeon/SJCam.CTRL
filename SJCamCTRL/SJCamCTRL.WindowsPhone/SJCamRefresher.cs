using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJCamCTRL.Models;
using Windows.UI.Xaml;
using System.Net.Http;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;

namespace SJCamCTRL
{
    public class SJCamRefresher
    {
        private readonly SJCam _camera;
        private DispatcherTimer _dispatcherTimer;
        private HttpClient _httpClient;

        public SJCamRefresher(SJCam camera)
        {
            _camera = camera;
            _httpClient = new HttpClient();

            _httpClient = new HttpClient(new HttpClientHandler());

            DispatcherTimerSetup();
        }

        public void DispatcherTimerSetup()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            //IsEnabled defaults to false
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            Refresh();
        }
        public async void Refresh()
        {
            _camera.IsConnected = await PingCamera();
            _camera.PhotosLeft = await GetPhotosLeft();
            _camera.VideoRecordingLeft = await GetVideoRecordingLeft();
        }
        
        private async Task<bool> PingCamera()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_camera.Url);

            return response.IsSuccessStatusCode;


        }

        private async Task<int> GetPhotosLeft()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_camera.Url}?custom=1&cmd={_camera.PhotosLeftCommand}");


            if (response.IsSuccessStatusCode)
            {
                int photosLeft;
                string responseString = await response.Content.ReadAsStringAsync();

                XDocument doc = XDocument.Parse(responseString);
                XmlDocument dox = new XmlDocument();
                dox.LoadXml(responseString);
                var node = dox.SelectSingleNode("Function/Value");

                bool resultParsed = int.TryParse(node.InnerText, out photosLeft);

                return resultParsed ? photosLeft : 0;
            }
            else
            {
            }


            return -1;
        }
        private async Task<int> GetVideoRecordingLeft()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_camera.Url}?custom=1&cmd={_camera.VideoRecordingLeftCommand}");


            if (response.IsSuccessStatusCode)
            {
                int photosLeft;
                string responseString = await response.Content.ReadAsStringAsync();

                XDocument doc = XDocument.Parse(responseString);
                XmlDocument dox = new XmlDocument();
                dox.LoadXml(responseString);
                var node = dox.SelectSingleNode("Function/Value");

                bool resultParsed = int.TryParse(node.InnerText, out photosLeft);

                return resultParsed ? photosLeft : 0;
            }
            else
            {
            }


            return -1;
        }
    }
}
