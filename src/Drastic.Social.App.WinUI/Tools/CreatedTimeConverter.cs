using Humanizer;
using Microsoft.UI.Xaml.Data;

namespace Drastic.Social.Tools;

public class CreatedTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var accountDateTime = (DateTime)value;
        return accountDateTime.Humanize(true, DateTime.UtcNow);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}