// <copyright file="ConsoleErrorLogHandler.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Services;

public class ConsoleErrorLogHandler : IErrorHandlerService
{
    /// <inheritdoc/>
    public void HandleError(Exception ex)
    {
        Console.Error.WriteLine(ex.ToString());
    }
}