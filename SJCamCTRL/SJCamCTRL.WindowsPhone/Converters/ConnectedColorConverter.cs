using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SJCamCTRL.Converters
{
    public class ConnectedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            bool isConnected = (bool)value;

            return new SolidColorBrush(isConnected ? Windows.UI.Colors.Green : Windows.UI.Colors.Red);
        }



        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }



    }
}
