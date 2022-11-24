// <copyright file="AuthorizationViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Tools;
using Mastonet;
using Mastonet.Entities;

namespace Drastic.Social.ViewModels
{
    /// <summary>
    /// Authorization Page View Model.
    /// </summary>
    public class AuthorizationViewModel : BaseViewModel
    {
        private MastodonClient? client;
        private Account? account;
        private string code;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public AuthorizationViewModel(IServiceProvider services)
            : base(services)
        {
            this.StartLoginCommand = new AsyncCommand(() => this.PerformBusyAsyncTask(this.SaveAndLoginAsync, Translations.Common.LoggingInText), () => (this.Account is not null && this.client is not null), this.Dispatcher, this.ErrorHandler);
            this.LoginViaCodeCommand = new AsyncCommand<string>((code) => this.PerformBusyAsyncTask(async () => await this.LoginViaCodeAsync(code), Translations.Common.LoggingInText), (code) => !string.IsNullOrEmpty(code), this.ErrorHandler);
        }

        /// <summary>
        /// Gets or sets the Account.
        /// </summary>
        public Account? Account
        {
            get => this.account;
            set => this.SetProperty(ref this.account, value);
        }

        /// <summary>
        /// Gets a value indicating whether to show the code input screen.
        /// Used if the redirect code is the default value.
        /// </summary>
        public bool ShowCodeScreen => this.Authorization.ShowCodeScreen;

        /// <summary>
        /// Gets or sets the server base url.
        /// </summary>
        public string Code
        {
            get => this.code;
            set
            {
                this.SetProperty(ref this.code, value);
            }
        }

        /// <summary>
        /// Gets the Login Via Code Command.
        /// </summary>
        public AsyncCommand<string> LoginViaCodeCommand { get; private set; }

        /// <summary>
        /// Gets the Start Login Command.
        /// </summary>
        public AsyncCommand StartLoginCommand { get; private set; }

        /// <inheritdoc/>
        public override void RaiseCanExecuteChanged()
        {
            base.RaiseCanExecuteChanged();
            this.LoginViaCodeCommand?.RaiseCanExecuteChanged();
            this.StartLoginCommand?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Login Via Code.
        /// </summary>
        /// <param name="code">Mastodon OAuth Code to login with.</param>
        /// <returns><see cref="Task"/>.</returns>
        private async Task LoginViaCodeAsync(string code)
        {
            if (this.Account != null)
            {
                return;
            }

            try
            {
                (this.client, this.Account) = await this.Authorization.LoginWithCodeAsync(code);
            }
            catch (Exception ex)
            {
                // If we can't show the auth screen, close the dialog and show the error.
                this.ErrorHandler.HandleError(ex);
            }
        }

        /// <summary>
        /// Save and Login Async.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        private Task SaveAndLoginAsync()
        {
            if (this.Account is null || this.client is null)
            {
                return Task.CompletedTask;
            }

            var mastoAccount = new Models.MastoUserAccount(this.Account, this.client, this.Authorization.AppRegistration!);
            this.Context.AddAccount(mastoAccount);

            return Task.CompletedTask;
        }
    }
}
