// <copyright file="Program.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using Drastic.Social.Services;
using Drastic.Social.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Sharprompt;

Console.WriteLine("Drastic.Social TestConsolematic-7000");

Ioc.Default.ConfigureServices(
    new ServiceCollection()
    .AddSingleton<IDatabaseContext>(new LiteDBDatabaseContext("testdatabase.db"))
    .AddSingleton<IErrorHandlerService, ConsoleErrorLogHandler>()
    .AddSingleton<IBrowserService, ConsoleBrowserService>()
    .AddSingleton<IAppDispatcher, ConsoleAppDispatcher>()
    .AddSingleton<IAuthorizationService>(provider => new AuthorizationService(provider.GetService<IBrowserService>()!, "Drastic.Social.Console"))
    .AddTransient<AuthorizationViewModel>()
    .AddTransient<LoginViewModel>()
    .AddTransient<UserProfileViewModel>()
    .AddTransient<HomeTootViewModel>()
    .AddTransient<PublicTootViewModel>()
    .BuildServiceProvider());

var database = Ioc.Default.GetService<IDatabaseContext>()!;

var loginDefault = database.GetDefaultAccount();

if (loginDefault is not null)
{
    var userProfileViewModel = Ioc.Default.ResolveWith<UserProfileViewModel>();
    await userProfileViewModel.OnLoad();
    foreach (var item in userProfileViewModel.Timeline!)
    {
        Console.WriteLine($"{item.Account.DisplayName} - {item.Content}");
    }

    var json = System.Text.Json.JsonSerializer.Serialize(userProfileViewModel.Timeline, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
    File.WriteAllText("UserProfileViewModel.json", json);
}
else
{
    await SetupLogin();
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

async Task SetupLogin()
{
    var loginViewModel = Ioc.Default.ResolveWith<LoginViewModel>();

    var authorizationViewModel = Ioc.Default.ResolveWith<AuthorizationViewModel>();

    loginViewModel.ServerBaseUrl = Prompt.Input<string>("Enter Instance Url", placeholder: loginViewModel.ServerBaseUrl);

    await loginViewModel.StartLoginCommand.ExecuteAsync();

    authorizationViewModel.Code = Prompt.Input<string>("Enter auth code");

    await authorizationViewModel.LoginViaCodeCommand.ExecuteAsync(authorizationViewModel.Code);

    Console.WriteLine(authorizationViewModel.Account!.UserName);

    await authorizationViewModel.StartLoginCommand.ExecuteAsync();

    var account = database.GetDefaultAccount();

    Console.WriteLine(account.Account!.UserName);
}
