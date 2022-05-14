using Geolocation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Weather.History.Entity;

using Windows.Devices.Geolocation;

namespace Weather.History.AppService
{
    public interface IGeoService
    {
        Task<GeoPoint> MyHomeLocation();
    }

    public class GeoLocator : IGeoService
    {
        readonly Geolocator _geolocator;

        public GeoLocator()
        {
            _geolocator = new Geolocator();
        }


        public async Task<GeoPoint> MyHomeLocation()
        {
            try
            {
                var status = await Geolocator.RequestAccessAsync();

                if (status == GeolocationAccessStatus.Allowed)
                {
                    await Geolocator.RequestAccessAsync();

                    var location = await _geolocator.GetGeopositionAsync();

                    return new GeoPoint
                    {
                        Latitude = location.Coordinate.Point.Position.Latitude.ToString(),
                        Longitude = location.Coordinate.Point.Position.Longitude.ToString()
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
