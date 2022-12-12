// <copyright file="StatusControl.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Social.Services;
using Drastic.Social.Tools;
using Mastonet.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.Social.App.WinUI.Controls
{
    public sealed partial class StatusControl : UserControl
    {
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(Status),
            typeof(StatusControl), new PropertyMetadata(null));

        private bool isBusy = false;

        public StatusControl()
        {
            this.InitializeComponent();
        }

        public Status Status
        {
            get
            {
                var value = (Status)this.GetValue(StatusProperty);
                return value;
            }

            set
            {
                this.SetValue(StatusProperty, value);
            }
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            if (!this.isBusy)
            {
                this.isBusy = true;

                var share = (IShareService)Ioc.Default.GetService(typeof(IShareService))!;
                share.ShareUrlAsync(this.Status.Uri).FireAndForgetSafeAsync();

                this.isBusy = false;
            }
        }
    }
}
