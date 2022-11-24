﻿// <copyright file="TootBaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet.Entities;
using Drastic.Social.Models;
using Drastic.Social.Tools;

namespace Drastic.Social.ViewModels
{
    /// <summary>
    /// Toot Base View Model.
    /// Used to host new Toot pages.
    /// </summary>
    public class TootBaseViewModel : BaseViewModel
    {
        private MastoUserAccount account;
        private Account? userAccount;
        private MastodonList<Status>? timeline;
        private TimelineType timelineType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TootBaseViewModel"/> class.
        /// </summary>
        /// <param name="service">IServiceProvider.</param>
        /// <param name="timelineType">TimelineType.</param>
        public TootBaseViewModel(IServiceProvider service, TimelineType timelineType)
            : base(service)
        {
            this.timelineType = timelineType;
            this.account = this.Context.GetDefaultAccount();
            this.UserAccount = this.Account.Account;
            this.ViewProfileCommand = new AsyncCommand<Account>(
                async (Account account) => await this.ExecuteViewProfileCommand(account),
                null,
                this.ErrorHandler);
        }

        /// <summary>
        /// Gets or sets the View Profile Command.
        /// </summary>
        public AsyncCommand<Account> ViewProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the Account.
        /// </summary>
        public MastoUserAccount Account
        {
            get => this.account;
            set => this.SetProperty(ref this.account, value);
        }

        /// <summary>
        /// Gets or sets the User Account.
        /// </summary>
        public Account? UserAccount
        {
            get => this.userAccount;
            set => this.SetProperty(ref this.userAccount, value);
        }

        /// <summary>
        /// Gets or sets the Timeline.
        /// </summary>
        public MastodonList<Status>? Timeline
        {
            get => this.timeline;
            set => this.SetProperty(ref this.timeline, value);
        }

        /// <summary>
        /// Refresh Toot Feed.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public async Task RefreshFeed()
        {
            this.IsBusy = true;
            switch (this.timelineType)
            {
                case TimelineType.Public:
                    this.Timeline = await this.Account.Client.GetPublicTimeline();
                    break;
                case TimelineType.Home:
                    this.Timeline = await this.Account.Client.GetHomeTimeline();
                    break;
                case TimelineType.User:
                    if (this.UserAccount != null)
                    {
                        this.Timeline = await this.Account.Client.GetAccountStatuses(this.UserAccount.Id);
                    }
                    else
                    {
                        this.Timeline = await this.Account.Client.GetAccountStatuses(this.Account.AccountId);
                    }

                    break;
            }

            this.IsBusy = false;
        }

        /// <inheritdoc/>
        public override void SetTitle(string title = "")
        {
            if (string.IsNullOrEmpty(title))
            {
                base.SetTitle(title);
                return;
            }

            switch (this.timelineType)
            {
                case TimelineType.Public:
                    break;
                case TimelineType.Home:
                    this.Title = "Home";
                    break;
                case TimelineType.List:
                    break;
                case TimelineType.User:
                    this.Title = this.UserAccount?.DisplayName ?? string.Empty;
                    break;
                default:
                    break;
            }
        }

        private Task ExecuteViewProfileCommand(Account account)
        {
            return Task.CompletedTask;
        }
    }
}
