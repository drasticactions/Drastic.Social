// <copyright file="IBrowserService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.Social.Services
{
    /// <summary>
    /// Browser Service.
    /// </summary>
    public interface IBrowserService
    {
        /// <summary>
        /// Open URL async.
        /// </summary>
        /// <param name="url">Url to open.</param>
        /// <returns>Task.</returns>
        public Task OpenAsync(string url);
    }
}
