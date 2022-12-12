// <copyright file="AddAtSignConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Data;

namespace Drastic.Social.Tools;

public class AddAtSignConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var accountNameValue = value as string;
        return accountNameValue == null ? "@error" : $"@{accountNameValue}";
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}