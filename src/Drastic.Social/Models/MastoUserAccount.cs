// <copyright file="MastoUserAccount.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using Mastonet;
using Mastonet.Entities;

namespace Drastic.Social.Models
{
    /// <summary>
    /// Mastodon User Account.
    /// </summary>
    public class MastoUserAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MastoUserAccount"/> class.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="client"></param>
        public MastoUserAccount(Account account, MastodonClient client, AppRegistration appRegistration)
        {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            ArgumentNullException.ThrowIfNull(account, nameof(account));
            ArgumentNullException.ThrowIfNull(appRegistration, nameof(appRegistration));
            this.Account = account;
            this.AccountId = this.Account.Id;
            this.Client = client;
            this.AppRegistration = appRegistration;
            this.AppRegistrationId = appRegistration.Id;
            this.UserAuth = UserAuth.GenerateUserAuth(this.AccountId, new Auth() { AccessToken = client.AccessToken });
            this.UserAuthId = this.Account.Id;
        }

        /// <summary>
        /// Gets or sets the Account Id.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the App Registration Id.
        /// </summary>
        public long AppRegistrationId { get; set; }

        /// <summary>
        /// Gets or sets the App Registration.
        /// </summary>
        public virtual AppRegistration? AppRegistration { get; set; }

        /// <summary>
        /// Gets or sets the Account.
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Gets or sets the User Auth Id.
        /// </summary>
        public string UserAuthId { get; set; }

        /// <summary>
        /// Gets or sets the UserAuth.
        /// </summary>
        public virtual UserAuth UserAuth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the account is the default.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the Client.
        /// </summary>
        [NotMapped]

        public MastodonClient Client { get; set; }
    }
}
