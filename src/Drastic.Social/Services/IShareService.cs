// <copyright file="IShareService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.Social.Services
{
    public interface IShareService
    {
        /// <summary>
        /// Share a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Task.</returns>
        Task ShareUrlAsync(string url);
    }
}
