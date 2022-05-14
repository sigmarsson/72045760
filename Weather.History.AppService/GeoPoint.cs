
namespace Weather.History.Entity
{
    public class GeoPoint
    {
        public GeoPoint()
        {

        }

        public GeoPoint(string lat, string lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public GeoPoint(double lat, double lon)
        {
            Latitude = lat.ToString();
            Longitude = lon.ToString();
        }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GeoPoint point && Latitude == point.Latitude && Longitude == point.Longitude;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"[{Latitude}, {Longitude}]";
        }

        public static bool operator ==(GeoPoint x, GeoPoint y)
        {
            return x.Longitude.Trim() == y.Longitude.Trim() && x.Latitude.Trim() == y.Latitude.Trim();
        }

        public static bool operator !=(GeoPoint x, GeoPoint y)
        {
            return !(x == y);
        }
    }
}
