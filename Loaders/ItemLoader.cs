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
using SoftRes.db;
using SoftRes.SharpMongo;

namespace SoftRes.Loaders
{
    public class ItemLoader : IHostedService
    {
        private readonly IBlizzardAuthHandler _authHandler;
        private readonly IBlizzardItemAPI _itemApiClient;
        private readonly IFileLoader _fileLoader;
        private readonly IMongoFactory _factory;
        public ItemLoader(
            IBlizzardAuthHandler authHandler, 
            IBlizzardItemAPI itemApiClient,
            IFileLoader fileLoader,
            IMongoFactory factory
        )
        {
            _authHandler = authHandler;
            _itemApiClient = itemApiClient;
            _fileLoader = fileLoader;
            _factory = factory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => DoWorkAsync(cancellationToken));

            return Task.CompletedTask;
        }

        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            // Bug with IHostedService
            await Task.Yield();

            var items = new List<Item>();
            var allIds = _fileLoader.GetIDs();

            foreach (var raidName in EnumHelpers.ToEnumerable<RaidInstance>())
            {
                if (allIds.ContainsKey(raidName))
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
            }

            _factory.Item.CreateMany(items);
            Console.WriteLine("Finished ingesting item data.");
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