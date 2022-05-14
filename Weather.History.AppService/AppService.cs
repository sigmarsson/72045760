using Grpc.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Weather.History.Abstract;

using Vanara.PInvoke;

namespace Weather.History.AppService
{
    public class AppService : gRpcAppService.gRpcAppServiceBase
    {
        readonly IntPtr _hwnd;
        readonly IGeoService _geoService;

        bool _started;

        public AppService(IntPtr hwnd, IGeoService geoService)
        {
            _hwnd = hwnd;
            _started = default;
            _geoService = geoService;
        }

        internal void Start()
        {
            _started = true;
        }

        internal void Stop()
        {
            _started = false;
        }

        public override Task<Position> GetPosition(RpcRequest request, ServerCallContext context)
        {
            return Task.Run(async () =>
            {
                var r = await _geoService.MyHomeLocation();

                return new Position
                {
                    Latitude = r?.Latitude,
                    Longitude = r?.Longitude
                };
            });
        }

        public override Task<RpcResponse> IsAlive(RpcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RpcResponse
            {
                Error = _started ? null : new gRpcException(),
            });
        }

        public override Task<RpcResponse> Unconceal(RpcRequest request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                var showmax = User32.ShowWindow(_hwnd, ShowWindowCommand.SW_SHOWMAXIMIZED);
                var show = User32.ShowWindow(_hwnd, ShowWindowCommand.SW_SHOW);
                var response = new RpcResponse();

                if (!showmax && show)
                {
                    response.Message = $"Product window has been unconcealed successfuly.";
                }
                else
                {
                    var message = $"Unable to unconceal product window";

                    response.Error = new gRpcException
                    {
                        Message = $"{message} :: Exception Message not available",
                        StackTrace = "StackTrace not available"
                    };
                }

                return response;
            });
        }
    }
}
