// <copyright file="ConsoleBrowserService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Diagnostics;
using Drastic.Social.Services;

public class ConsoleBrowserService : IBrowserService
{
    public Task OpenAsync(string url)
    {
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        return Task.CompletedTask;
    }
}