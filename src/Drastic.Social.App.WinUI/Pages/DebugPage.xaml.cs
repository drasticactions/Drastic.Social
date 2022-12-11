// <copyright file="DebugPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Social.Mocks;
using Drastic.Social.Tools;

namespace Drastic.Social.App.WinUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DebugPage : BasePage
    {
        public DebugViewModel Vm;

        public DebugPage()
        {
            this.InitializeComponent();
            this.DataContext = this.Vm = Ioc.Default.ResolveWith<DebugViewModel>();
        }
    }
}
