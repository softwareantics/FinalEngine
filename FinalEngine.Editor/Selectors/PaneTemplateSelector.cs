// <copyright file="PaneTemplateSelector.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Selectors
{
    using System.Windows;
    using System.Windows.Controls;
    using FinalEngine.Editor.ViewModels;

    public class PaneTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ConsoleViewTemplate { get; set; }

        public DataTemplate ProjectExplorerViewTemplate { get; set; }

        public DataTemplate PropertiesViewTemplate { get; set; }

        public DataTemplate SceneHierarchyViewTemplate { get; set; }

        public DataTemplate SceneViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IConsoleViewModel)
            {
                return this.ConsoleViewTemplate;
            }
            else if (item is IProjectExplorerViewModel)
            {
                return this.ProjectExplorerViewTemplate;
            }
            else if (item is ISceneHierarchyViewModel)
            {
                return this.SceneHierarchyViewTemplate;
            }
            else if (item is IPropertiesViewModel)
            {
                return this.PropertiesViewTemplate;
            }
            else if (item is ISceneViewModel)
            {
                return this.SceneViewTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}