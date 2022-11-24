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
    .BuildServiceProvider());

var loginViewModel = Ioc.Default.ResolveWith<LoginViewModel>();

var authorizationViewModel = Ioc.Default.ResolveWith<AuthorizationViewModel>();

loginViewModel.ServerBaseUrl = Prompt.Input<string>("Enter Instance Url", placeholder: loginViewModel.ServerBaseUrl);

await loginViewModel.StartLoginCommand.ExecuteAsync();

authorizationViewModel.Code = Prompt.Input<string>("Enter auth code");

await authorizationViewModel.LoginViaCodeCommand.ExecuteAsync(authorizationViewModel.Code);

Console.WriteLine(authorizationViewModel.Account!.UserName);