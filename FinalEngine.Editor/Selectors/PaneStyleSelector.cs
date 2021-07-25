// <copyright file="PaneStyleSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Selectors
{
    using System.Windows;
    using System.Windows.Controls;
    using FinalEngine.Editor.ViewModels;

    public class PanesStyleSelector : StyleSelector
    {
        public Style DocumentStyle { get; set; }

        public Style ToolStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is IToolViewModel)
            {
                return this.ToolStyle;
            }

            if (item is IPaneViewModel)
            {
                return this.DocumentStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
}