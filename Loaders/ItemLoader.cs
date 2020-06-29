using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SoftRes.Auth;
using SoftRes.BlizzardAPI;
using SoftRes.Models;

namespace SoftRes.Loaders
{
    public class ItemLoader : IHostedService
    {
        private readonly IBlizzardAuthHandler _authHandler;
        private readonly IBlizzardItemAPI _itemApiClient;
        private int[] _itemIds = { 18861, 17077, 12640 };

        public ItemLoader(
            IBlizzardAuthHandler authHandler, 
            IBlizzardItemAPI itemApiClient
        )
        {
            _authHandler = authHandler;
            _itemApiClient = itemApiClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var items = new List<Item>();

            foreach (var id in _itemIds)
            {
                items.Add(
                    await _itemApiClient
                        .ItemId(id)
                        .Namespace("static-classic-us")
                        .Locale("en-US")
                        .Execute()
                );
            }

            var test = "";
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cancellationToken = cts.Token;

            cts.Cancel();

            return Task.CompletedTask;
        }
    }
}