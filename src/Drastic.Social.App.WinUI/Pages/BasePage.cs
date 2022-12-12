// <copyright file="BasePage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.Tools;
using Drastic.Social.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.Social.App.WinUI.Pages
{
    public class BasePage : Page, IDisposable
    {
        internal bool DisposedValue;

        public BasePage()
        {
            this.Loaded += this.BasePage_Loaded;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.DisposedValue)
            {
                if (disposing)
                {
                    this.Loaded -= this.BasePage_Loaded;
                }

                this.DisposedValue = true;
            }
        }

        private void BasePage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (this.DataContext is BaseViewModel vm)
            {
                vm.OnLoad().FireAndForgetSafeAsync();
            }
        }
    }
}
