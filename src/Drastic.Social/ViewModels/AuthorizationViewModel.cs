// <copyright file="AuthorizationViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Tools;
using Drastic.Mastodon;
using Drastic.Mastodon.Entities;

namespace Drastic.Social.ViewModels
{
    /// <summary>
    /// Authorization Page View Model.
    /// </summary>
    public class AuthorizationViewModel : BaseViewModel
    {
        private MastodonClient? client;
        private Account? account;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public AuthorizationViewModel(IServiceProvider services)
            : base(services)
        {
            this.StartLoginCommand = new AsyncCommand(async () => await this.SaveAndLoginAsync(), () => true, this.Dispatcher,this.ErrorHandler);
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
        /// Gets the Start Login Command.
        /// </summary>
        public AsyncCommand StartLoginCommand { get; private set; }

        /// <summary>
        /// Login Via Code.
        /// </summary>
        /// <param name="code">Mastodon OAuth Code to login with.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task LoginViaCodeAsync(string code)
        {
            if (this.IsBusy || this.Account != null)
            {
                return;
            }

            try
            {
                this.IsBusy = true;
                (this.client, this.Account) = await this.Authorization.LoginWithCodeAsync(code);
                this.IsBusy = false;
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
        public async Task SaveAndLoginAsync()
        {
            if (this.Account is null || this.client is null)
            {
                return;
            }

            this.IsBusy = true;
            var mastoAccount = new Models.MastoUserAccount(this.Account, this.client);
            this.Context.AddAccount(mastoAccount);
            this.IsBusy = false;
        }
    }
}
