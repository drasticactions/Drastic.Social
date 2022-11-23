// <copyright file="MastoUserAccount.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;
using Drastic.Mastodon;
using Drastic.Mastodon.Entities;

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
        public MastoUserAccount(Account account, MastodonClient client)
        {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            ArgumentNullException.ThrowIfNull(account, nameof(account));
            ArgumentNullException.ThrowIfNull(client.AuthToken, nameof(client.AuthToken));
            ArgumentNullException.ThrowIfNull(client.AppRegistration, nameof(client.AppRegistration));
            this.Account = account;
            this.AccountId = this.Account.Id;
            this.Client = client;
            this.AppRegistration = this.Client.AppRegistration;
            this.AppRegistrationId = this.Client.AppRegistration.Id;
            this.UserAuth = UserAuth.GenerateUserAuth(this.AccountId, this.Client.AuthToken);
            this.UserAuthId = this.Account.Id;
        }

        /// <summary>
        /// Gets or sets the Account Id.
        /// </summary>
        public long AccountId { get; set; }

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
        public long UserAuthId { get; set; }

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
