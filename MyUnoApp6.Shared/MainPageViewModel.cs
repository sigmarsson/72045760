using System;
using System.Threading.Tasks;
using System.Text;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Regions;

using Weather.History.AppService;

namespace MyUnoApp6
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel(IGeoService geoService)
        {
            Task.Run(async () =>
            {
                var p = await geoService.MyHomeLocation();
            });
        }
    }
}
