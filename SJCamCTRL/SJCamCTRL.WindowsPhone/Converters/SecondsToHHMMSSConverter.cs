using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SJCamCTRL.Converters
{
    public class SecondsToHHMMSSConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            int ss = (int)value;

            var tSpan = new TimeSpan(0, 0, ss);
            return string.Format(@"{0:D2}:{1:D2}:{1:D2}", tSpan.Hours, tSpan.Minutes, tSpan.Seconds);
        }



        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }



    }
}
