// <copyright file="IEventsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

public interface IEventsProcessor
{
    bool CanProcessEvents { get; }

    void ProcessEvents();
}
