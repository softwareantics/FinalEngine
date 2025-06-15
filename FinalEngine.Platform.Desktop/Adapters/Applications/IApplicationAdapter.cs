// <copyright file="IApplicationAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters.Applications;

internal interface IApplicationAdapter
{
    bool FilterMessage(ref Message message);
}
