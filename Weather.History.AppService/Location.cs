using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Weather.History.Entity
{
    [DataContract]
    public class Location : IEquatable<Location>
    {
        public Location()
        {

        }

        public Location(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude.ToString();
            Longitude = longitude.ToString();
        }

        public Location(double latitude, double longitude, string name, string state) : this(latitude, longitude)
        {
            AsciiName = name;
            State = state;
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string AsciiName { get; set; }

        [DataMember]
        public string Latitude { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string CountryCode { get; set; }

        public bool Equals(Location other)
        {
            return ReferenceEquals(this, other) || Latitude == other.Latitude && Longitude == other.Longitude;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"{AsciiName}, {State} {CountryCode}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }
    }
}
