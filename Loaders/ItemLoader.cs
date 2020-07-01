using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SoftRes.Auth;
using SoftRes.BlizzardAPI.Items;
using SoftRes.Models;
using SoftRes.Helpers;

namespace SoftRes.Loaders
{
    public class ItemLoader : IHostedService
    {
        private readonly IBlizzardAuthHandler _authHandler;
        private readonly IBlizzardItemAPI _itemApiClient;
        private readonly IFileLoader _fileLoader;
        private string[] _mcItemIds;

        public ItemLoader(
            IBlizzardAuthHandler authHandler, 
            IBlizzardItemAPI itemApiClient,
            IFileLoader fileLoader
        )
        {
            _authHandler = authHandler;
            _itemApiClient = itemApiClient;
            _fileLoader = fileLoader;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var items = new List<Item>();
            var allIds = _fileLoader.GetIDs();

            foreach (var raidName in EnumHelpers.ToEnumerable<RaidInstance>())
            {
                foreach (var itemId in allIds[raidName])
                {
                    var item = Item(
                        await _itemApiClient
                            .ItemId(Convert.ToInt32(itemId))
                            .Namespace("static-classic-us")
                            .Locale("en_US")
                            .Execute(), 
                        raidName
                    );
                    items.Add(item);

                    Console.WriteLine($"Item: {item.Name} imported.");
                }
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

        private static Item Item(Item item, RaidInstance instance)
        {
            item.Instance = instance;

            return item;
        }
    }
}