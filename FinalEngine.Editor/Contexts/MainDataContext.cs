// <copyright file="MainDataContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.Commands;

    public class MainDataContext : DataContextBase, IMainDataContext
    {
        private readonly ICommand closeCommand;

        private bool isClosed;

        public MainDataContext(ISceneDataContext sceneDataContext)
        {
            this.SceneDataContext = sceneDataContext ?? throw new ArgumentNullException(nameof(sceneDataContext), $"The specified {nameof(sceneDataContext)} parameter cannot be null.");
        }

        public ICommand CloseCommand
        {
            get { return this.closeCommand ?? new RelayCommand(o => this.Close()); }
        }

        public bool IsClosed
        {
            get
            {
                return this.isClosed;
            }

            private set
            {
                if (this.isClosed != value)
                {
                    this.isClosed = value;
                    this.InvokePropertyChanged(nameof(this.IsClosed));
                }
            }
        }

        public ISceneDataContext SceneDataContext { get; }

        private void Close()
        {
            this.IsClosed = true;
        }
    }
}