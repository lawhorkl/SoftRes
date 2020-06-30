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

namespace SoftRes.Loaders
{
    public class ItemLoader : IHostedService
    {
        private readonly IBlizzardAuthHandler _authHandler;
        private readonly IBlizzardItemAPI _itemApiClient;
        private string[] _mcItemIds;

        public ItemLoader(
            IBlizzardAuthHandler authHandler, 
            IBlizzardItemAPI itemApiClient
        )
        {
            _authHandler = authHandler;
            _itemApiClient = itemApiClient;

            var workingDirectory = Directory.GetCurrentDirectory();
            var fileName = "MC.txt";
            var path = String.Join('/', workingDirectory, "IDs", fileName);
            _mcItemIds = File.ReadAllLines(path).Distinct().ToArray();
            
            var test = "";
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var items = new List<Item>();

            foreach (var id in _mcItemIds)
            {
                var item = Item(
                    await _itemApiClient
                        .ItemId(Convert.ToInt32(id))
                        .Namespace("static-classic-us")
                        .Locale("en_US")
                        .Execute(), 
                    RaidInstance.MoltenCore
                );
                items.Add(item);

                Console.WriteLine($"Item: {item.Name} imported.");
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