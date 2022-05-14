using GrpcDotNetNamedPipes;

using System;
using System.Threading.Tasks;
using Weather.History.Abstract;
using Weather.History.Entity;

namespace Weather.History.AppService
{
    public sealed class AppServiceHost : IDisposable, IProductService
    {
        readonly NamedPipeServer _server;
        readonly AppService _service;

        public AppServiceHost(IntPtr hWnd, IGeoService myLocation)
        {
            _server = new NamedPipeServer(Environment.RootDir);
            _service = new AppService(hWnd, myLocation);

            gRpcAppService.BindService(_server.ServiceBinder, _service);
        }

        public string Address => $"net.pipe://{Environment.RootDir}";

        public void Dispose()
        {
            _service.Stop();
            _server.Dispose();
        }

        public void Start()
        {
            _server.Start();
            _service.Start();
        }

        public bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public void Unconceal()
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetLocation()
        {
            return Task.Run(async () =>
            {
                var position = await _service.GetPosition(new RpcRequest(), null);

                return new Location(position.Latitude, position.Longitude);
            });
        }
    }
}
