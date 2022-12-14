// <copyright file="UserAuth.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet;
using Mastonet.Entities;

namespace Drastic.Social.Models
{
    /// <summary>
    /// User Auth.
    /// </summary>
    public class UserAuth
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the Access Token.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Token Type.
        /// </summary>
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Scope.
        /// </summary>
        public string Scope { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Create At time.
        /// </summary>
        public string CreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// Generates Auth Values.
        /// </summary>
        /// <param name="auth"><see cref="UserAuth"/>.</param>
        /// <returns><see cref="Auth"/>.</returns>
        public static Auth GenerateAuth(UserAuth auth)
        {
            return new Auth()
            {
                AccessToken = auth.AccessToken,
                TokenType = auth.TokenType,
                Scope = auth.Scope,
                CreatedAt = auth.CreatedAt,
            };
        }

        /// <summary>
        /// Generates UserAuth values.
        /// </summary>
        /// <param name="accountId">Account Id.</param>
        /// <param name="auth"><see cref="Auth"/>.</param>
        /// <returns><see cref="UserAuth"/>.</returns>
        public static UserAuth GenerateUserAuth(string accountId, Auth auth)
        {
            return new UserAuth()
            {
                Id = accountId,
                AccessToken = auth.AccessToken,
                CreatedAt = auth.CreatedAt,
                Scope = auth.Scope,
                TokenType = auth.TokenType,
            };
        }
    }
}
