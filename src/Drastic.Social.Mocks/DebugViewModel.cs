// <copyright file="DebugViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Social.ViewModels;
using Mastonet.Entities;
using System.Collections.ObjectModel;

namespace Drastic.Social.Mocks
{
    public class DebugViewModel : BaseViewModel
    {
        public DebugViewModel(IServiceProvider services)
            : base(services)
        {
        }

        public ObservableCollection<Status> Status { get; } = new ObservableCollection<Status>();

        public override async Task OnLoad()
        {
            await base.OnLoad();
            var statuses = AssemblyFileLoader.DeserializeViaResourceFile<List<Status>>("Json.PublicTootViewModel.json");
            foreach (var status in statuses)
            {
                this.Status.Add(status);
            }
        }
    }
}
