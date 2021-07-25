// <copyright file="ToolViewModelBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    public abstract class ToolViewModelBase : PaneViewModelBase, IToolViewModel
    {
        private bool isVisible;

        public ToolViewModelBase()
        {
            this.Title = "Tool Window";
            this.IsVisible = true;
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                if (this.isVisible == value)
                {
                    return;
                }

                this.isVisible = value;
                this.InvokePropertyChanged(nameof(this.IsVisible));
            }
        }
    }
}