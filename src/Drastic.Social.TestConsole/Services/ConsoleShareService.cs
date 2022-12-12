// <copyright file="ConsoleShareService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Services;

namespace Drastic.Social.Services
{
    public class ConsoleShareService : IShareService
    {
        /// <inheritdoc/>
        public Task ShareUrlAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
