// <copyright file="SettingsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Mastodon.Entities;

namespace Drastic.Social.ViewModels
{
    /// <summary>
    /// Settings View Model.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public SettingsViewModel(IServiceProvider services)
            : base(services)
        {
            this.TestStatus = new Status()
            {
                Account = new Account()
                {
                    HeaderUrl = "https://files.mastodon.social/accounts/headers/000/458/416/original/1b44325f7ccb0b37.jpg",
                    AvatarUrl = "https://files.mastodon.social/accounts/avatars/000/458/416/original/c751c2d7145c883e.png",
                    UserName = "Drastic Actions",
                    AccountName = "drasticactions",
                },
                Content = "<p>Test Content! This is test content! Look at me! Test!</p>",
                CreatedAt = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Gets the Test Status.
        /// </summary>
        public Status TestStatus { get; private set; }
    }
}
