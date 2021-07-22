// <copyright file="DataContextBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    using System;
    using System.ComponentModel;

    public abstract class DataContextBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void InvokePropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(nameof(propertyName)))
            {
                throw new ArgumentNullException(nameof(propertyName), $"The specified {nameof(propertyName)} parameter cannot be null.");
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}