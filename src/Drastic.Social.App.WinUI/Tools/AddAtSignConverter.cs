using Microsoft.UI.Xaml.Data;

namespace Drastic.Social.Tools;

public class AddAtSignConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var accountNameValue = value as string;
        return accountNameValue == null ? "@error" : $"@{accountNameValue}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}