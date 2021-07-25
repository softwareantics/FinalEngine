// <copyright file="IPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    public interface IPaneViewModel
    {
        string ContentID { get; set; }

        bool IsActive { get; set; }

        bool IsSelected { get; set; }

        string Title { get; set; }
    }
}