// <copyright file="StatusControl.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mastonet.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.Social.App.WinUI.Controls
{
    public sealed partial class StatusControl : UserControl
    {
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(Status),
            typeof(StatusControl), new PropertyMetadata(null));

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
    }
}
