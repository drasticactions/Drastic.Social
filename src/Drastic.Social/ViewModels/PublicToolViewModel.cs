// <copyright file="PublicToolViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Models;

namespace Drastic.Social.ViewModels
{
    /// <summary>
    /// Public Toot View Model.
    /// </summary>
    public class PublicTootViewModel : TootBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicTootViewModel"/> class.
        /// </summary>
        /// <param name="service">IServiceProvider.</param>
        public PublicTootViewModel(IServiceProvider service)
            : base(service, TimelineType.Public)
        {
        }
    }
}
