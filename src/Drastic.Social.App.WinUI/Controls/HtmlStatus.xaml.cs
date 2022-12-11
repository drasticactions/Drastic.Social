// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Mastonet.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Drastic.Social.App.WinUI.Controls
{
    public sealed partial class HtmlStatus : UserControl
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string),
          typeof(HtmlStatus), new PropertyMetadata(null));

        public HtmlStatus()
        {
            this.InitializeComponent();
        }

        public string Html
        {
            get { return (string)this.GetValue(HtmlProperty); }

            set
            {
                this.SetValue(HtmlProperty, value);
                this.SetupHtml(value);
            }
        }

        private void SetupHtml(string text)
        {
            // Just in case we are not given text with elements.
            var modifiedText = string.Format("<div>{0}</div>", text);
            modifiedText = Regex.Replace(modifiedText, "<br>", "<br></br>", RegexOptions.IgnoreCase);

            // Reset the text because we will add to it.
            this.InnerTextBlock.Inlines.Clear();

            try
            {
                var element = XElement.Parse(modifiedText);
                LabelHtmlHelper.ParseText(element, this.InnerTextBlock.Inlines);
            }
            catch (Exception)
            {
                // If anything goes wrong just show the html
                this.InnerTextBlock.Text = global::Windows.Data.Html.HtmlUtilities.ConvertToText(text);
            }
        }
    }
}
