// <copyright file="UserProfileViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Models;
using Drastic.Mastodon.Entities;

namespace Drastic.Social.ViewModels
{
    /// <summary>
    /// User Profile View Model.
    /// </summary>
    public class UserProfileViewModel : TootBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileViewModel"/> class.
        /// </summary>
        /// <param name="service">IServiceProvider.</param>
        public UserProfileViewModel(IServiceProvider service)
            : base(service, TimelineType.User)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileViewModel"/> class.
        /// </summary>
        /// <param name="userAccount">User Account.</param>
        /// <param name="service">IServiceProvider.</param>
        public UserProfileViewModel(Account userAccount, IServiceProvider service)
            : base(service, TimelineType.User)
        {
            this.UserAccount = userAccount;
        }
    }
}
