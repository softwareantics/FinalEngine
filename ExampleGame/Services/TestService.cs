// <copyright file="TestService.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace ExampleGame.Services;

using Microsoft.Extensions.Logging;

internal sealed class TestService : ITestService
{
    private readonly ILogger<TestService> logger;

    public TestService(ILogger<TestService> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void DoSomething()
    {
        this.logger.LogWarning("Totally doing something awesome.");
    }
}
