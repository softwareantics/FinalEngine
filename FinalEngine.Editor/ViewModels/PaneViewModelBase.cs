// <copyright file="PaneViewModelBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    public abstract class PaneViewModelBase : ViewModelBase, IPaneViewModel
    {
        private string contentID;

        private bool isActive;

        private bool isSelected;

        private string title;

        public string ContentID
        {
            get
            {
                return this.contentID;
            }

            set
            {
                if (this.contentID == value)
                {
                    return;
                }

                this.contentID = value;
                this.InvokePropertyChanged(nameof(this.ContentID));
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                if (this.isActive == value)
                {
                    return;
                }

                this.isActive = value;
                this.InvokePropertyChanged(nameof(this.IsActive));
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected == value)
                {
                    return;
                }

                this.isSelected = true;
                this.InvokePropertyChanged(nameof(this.IsSelected));
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.title == value)
                {
                    return;
                }

                this.title = value;
                this.InvokePropertyChanged(nameof(this.Title));
            }
        }
    }
}