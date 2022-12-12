// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Social.App.WinUI.Pages;
using Drastic.Social.App.WinUI.Services;
using Drastic.Social.Mocks;
using Drastic.Social.Services;
using Drastic.Social.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Drastic.Social.App.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window window;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
            .AddSingleton<IDatabaseContext>(new LiteDBDatabaseContext(Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "testdatabase.db")))
            .AddSingleton<IErrorHandlerService, ErrorHandlerService>()
            .AddSingleton<IBrowserService, BrowserService>()
            .AddSingleton<IShareService, ShareService>()
            .AddSingleton<IAppDispatcher>(provider => new WinUIAppDispatcher(dispatcherQueue))
            .AddSingleton<IAuthorizationService>(provider => new AuthorizationService(provider.GetService<IBrowserService>()!, "Drastic.Social.WinUI"))
            .AddTransient<AuthorizationViewModel>()
            .AddTransient<LoginViewModel>()
            .AddTransient<UserProfileViewModel>()
            .AddTransient<HomeTootViewModel>()
            .AddTransient<PublicTootViewModel>()
            .AddTransient<DebugViewModel>()
            .BuildServiceProvider());
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            this.window = new MainWindow(new DebugPage());
            this.window.Activate();
        }
    }
}
