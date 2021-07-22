// <copyright file="MainView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views
{
    using System;
    using System.Windows;
    using FinalEngine.Editor.Contexts;

    /// <summary>
    ///   Interaction logic for MainView.xaml.
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IMainDataContext dataContext)
        {
            this.InitializeComponent();
            this.DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext), $"The specified {nameof(dataContext)} parameter cannot be null.");
        }
    }
}