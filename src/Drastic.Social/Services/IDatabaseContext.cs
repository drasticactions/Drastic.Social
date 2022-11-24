// <copyright file="IDatabaseContext.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Models;

namespace Drastic.Social.Services
{
    /// <summary>
    /// Database Context.
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Gets a boolean to indicating if this install has accounts loaded.
        /// </summary>
        /// <returns>Boolean.</returns>
        public bool HasAccount();

        /// <summary>
        /// Gets the default account from the database context.
        /// </summary>
        /// <returns><see cref="MastoUserAccount"/>.</returns>
        public MastoUserAccount GetDefaultAccount();

        /// <summary>
        /// Adds a new account to the database context.
        /// </summary>
        /// <param name="account"><see cref="MastoUserAccount"/>.</param>
        /// <returns>The same <see cref="MastoUserAccount"/>.</returns>
        public MastoUserAccount AddAccount(MastoUserAccount account);

        /// <summary>
        /// Gets an account from the database context.
        /// </summary>
        /// <param name="id">The account id.</param>
        /// <returns><see cref="MastoUserAccount"/>.</returns>
        public MastoUserAccount GetAccount(string id);
    }
}
