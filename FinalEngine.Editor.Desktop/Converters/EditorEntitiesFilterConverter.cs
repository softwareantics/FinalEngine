// <copyright file="EditorEntitiesFilterConverter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Converters;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Components;

internal sealed class EditorEntitiesFilterConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<Entity> entities)
        {
            var view = CollectionViewSource.GetDefaultView(entities);

            view.Filter ??= o =>
            {
                var e = (Entity)o;
                return !e.ContainsComponent<EditorComponent>();
            };

            return view;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
