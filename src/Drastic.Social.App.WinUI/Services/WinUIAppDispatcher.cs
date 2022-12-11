﻿// <copyright file="WinUIAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Services;
using Microsoft.UI.Dispatching;

namespace Drastic.Social.App.WinUI.Services
{
    public class WinUIAppDispatcher : IAppDispatcher
    {
        private readonly DispatcherQueue dispatcherQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinUIAppDispatcher"/> class.
        /// </summary>
        /// <param name="dispatcherQueue">Dispatcher Queue.</param>
        public WinUIAppDispatcher(DispatcherQueue dispatcherQueue)
        {
            this.dispatcherQueue = dispatcherQueue ?? throw new ArgumentNullException(nameof(dispatcherQueue));
        }

        /// <inheritdoc/>
        public bool Dispatch(Action action)
        {
            _ = action ?? throw new ArgumentNullException(nameof(action));
            return this.dispatcherQueue.TryEnqueue(() => action());
        }
    }
}
