﻿using System;
using System.Globalization;
using System.Security;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Converts text of a TextBox to Visibility
    /// </summary>
    public class TextToVisibilityConverter : BaseValueConverter<TextToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String && String.IsNullOrEmpty((value as String)))
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}