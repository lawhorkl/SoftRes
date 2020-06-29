using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SoftRes.Auth;

namespace SoftRes.Loaders
{
    public class ItemLoader : IHostedService
    {
        private readonly IBlizzardAuthHandler _authHandler;
        private int[] _itemIds = { 18861, 17077, 12640 };

        public ItemLoader(IBlizzardAuthHandler authHandler)
        {
            _authHandler = authHandler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
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