// <copyright file="IDockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System.Collections.Generic;

    public interface IDockViewModel
    {
        IEnumerable<IToolViewModel> Anchorables { get; }

        IEnumerable<IPaneViewModel> Documents { get; }
    }
}