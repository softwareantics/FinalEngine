// <copyright file="BoolToVisibilityConverter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsInverted = parameter == null ? false : (bool)parameter;
            bool IsVisible = value == null ? false : (bool)value;
            if (IsVisible)
            {
                return IsInverted ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                return IsInverted ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visiblility = value == null ? Visibility.Hidden : (Visibility)value;
            bool IsInverted = parameter == null ? false : (bool)parameter;

            return (visiblility == Visibility.Visible) != IsInverted;
        }
    }
}