// <copyright file="BrowserService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Services;

namespace Drastic.Social.App.WinUI.Services
{
    internal class BrowserService : IBrowserService
    {
        public async Task OpenAsync(string url)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
