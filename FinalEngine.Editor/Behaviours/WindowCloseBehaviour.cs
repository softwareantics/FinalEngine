// <copyright file="WindowCloseBehaviour.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Behaviours
{
    using System.Windows;
    using Microsoft.Xaml.Behaviors;

    public class WindowCloseBehaviour : Behavior<Window>
    {
        public static readonly DependencyProperty CloseTriggerProperty = DependencyProperty.Register(
            "CloseTrigger",
            typeof(bool),
            typeof(WindowCloseBehaviour),
            new PropertyMetadata(false, OnCloseTriggerChanged));

        public bool CloseTrigger
        {
            get { return (bool)this.GetValue(CloseTriggerProperty); }
            set { this.SetValue(CloseTriggerProperty, value); }
        }

        private static void OnCloseTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WindowCloseBehaviour behaviour)
            {
                behaviour.OnCloseTriggerChanged();
            }
        }

        private void OnCloseTriggerChanged()
        {
            if (this.CloseTrigger)
            {
                this.AssociatedObject.Close();
            }
        }
    }
}