// <copyright file="AuthorizationService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Mastonet;
using Mastonet.Entities;

namespace Drastic.Social.Services
{
    /// <summary>
    /// Authorization Service.
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string codeScreen = "urn:ietf:wg:oauth:2.0:oob";
        private string redirectUrl;

        private string hostUrl = string.Empty;
        private AppRegistration? appRegistration;
        private AuthenticationClient? authClient;
        private IBrowserService launcher;
        private string appName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
        /// </summary>
        /// <param name="browserService"><see cref="IBrowserService"/>.</param>
        /// <param name="redirectUrl">The redirect URL to go to.</param>
        /// <param name="appName">The default app name to show on the Oauth Screen.</param>
        public AuthorizationService(IBrowserService browserService, string appName = "Drastic.Social", string? redirectUrl = null)
        {
            this.appName = appName;
            this.redirectUrl = redirectUrl ?? this.codeScreen;
            this.launcher = browserService;
        }

        /// <inheritdoc/>
        public AppRegistration? AppRegistration => this.appRegistration;

        /// <inheritdoc/>
        public bool ShowCodeScreen => this.redirectUrl == this.codeScreen;

        /// <inheritdoc/>
        public async Task SetupLogin(string serverBase)
        {
            this.appRegistration = await this.GetAppRegistrationAsync(serverBase);
            this.authClient = new AuthenticationClient(this.appRegistration);
            var oauthUrl = this.authClient.OAuthUrl(this.redirectUrl);
            await this.launcher.OpenAsync(oauthUrl);
        }

        /// <inheritdoc/>
        public async Task<(MastodonClient Client, Account Account)> LoginWithCodeAsync(string code)
        {
            ArgumentNullException.ThrowIfNull(this.authClient, nameof(this.authClient));
            ArgumentNullException.ThrowIfNull(this.appRegistration, nameof(this.appRegistration));

            var auth = await this.authClient.ConnectWithCode(code, this.redirectUrl);
            var client = new MastodonClient(this.appRegistration.Instance, auth.AccessToken);
            var account = await client.GetCurrentUser();
            return (client, account);
        }

        private async Task<AppRegistration> GetAppRegistrationAsync(string serverBase)
        {
            Uri.TryCreate(serverBase, UriKind.RelativeOrAbsolute, out Uri? serverBaseUri);
            if (serverBaseUri == null)
            {
                throw new InvalidServerUriException(serverBase);
            }

            this.hostUrl = serverBaseUri.IsAbsoluteUri ? serverBaseUri.Host : serverBaseUri.OriginalString;
            AppRegistration appRegistration;
            try
            {
                var initAuthClient = new AuthenticationClient(this.hostUrl);
                appRegistration = await initAuthClient.CreateApp(this.appName, Scope.Read | Scope.Write | Scope.Follow, null, this.redirectUrl);
            }
            catch (Exception ex)
            {
                throw new AppRegistrationCreationFailureException(ex.Message, ex);
            }

            return appRegistration;
        }
    }
}
