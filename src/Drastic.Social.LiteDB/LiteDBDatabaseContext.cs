// <copyright file="LiteDBDatabaseContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Drastic.Social.Models;
using LiteDB;

namespace Drastic.Social.Services
{
    /// <summary>
    /// App Dispatcher.
    /// </summary>
    public class LiteDBDatabaseContext : IDatabaseContext
    {
        private const string UseraccountDB = "useraccounts";

        private LiteDatabase db;
        private string databasePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDBDatabaseContext"/> class.
        /// </summary>
        /// <param name="dbPath">Database Path.</param>
        public LiteDBDatabaseContext(string dbPath)
        {
            this.databasePath = dbPath;
            this.db = new LiteDatabase(this.databasePath);
        }

        /// <summary>
        /// Gets the User Accounts.
        /// </summary>
        public ILiteCollection<MastoUserAccount> UserAccounts => this.db.GetCollection<MastoUserAccount>(UseraccountDB);

        /// <inheritdoc/>
        public MastoUserAccount AddAccount(MastoUserAccount account)
        {
            if (!this.UserAccounts.FindAll().Any())
            {
                account.IsDefault = true;
            }

            if (!this.UserAccounts.FindAll().Any(n => n.AccountId == account.AccountId))
            {
                this.UserAccounts.Insert(account);
            }

            return account;
        }

        /// <inheritdoc/>
        public MastoUserAccount GetAccount(string id)
        {
            var account = this.UserAccounts.Include(n => n.UserAuth).Include(n => n.AppRegistration).FindOne(n => n.AccountId == id);
            if (account != null)
            {
                account.Client = new MastodonClient(account.AppRegistration!.Instance, UserAuth.GenerateAuth(account.UserAuth).AccessToken);
            }

            return account!;
        }

        /// <inheritdoc/>
        public MastoUserAccount GetDefaultAccount()
        {
            var account = this.UserAccounts.Include(n => n.UserAuth).Include(n => n.AppRegistration).FindOne(n => n.IsDefault);
            if (account != null)
            {
                account.Client = new MastodonClient(account.AppRegistration!.Instance, UserAuth.GenerateAuth(account.UserAuth).AccessToken);
            }

            return account!;
        }

        /// <inheritdoc/>
        public bool HasAccount()
        {
            return this.UserAccounts.FindAll().Any();
        }
    }
}