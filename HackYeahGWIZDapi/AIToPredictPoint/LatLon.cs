using HackYeahGWIZDapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AIToPredictPoint
{
    public class LatLon
    {
        private const double EARTH_RADIUS = 6371.0;
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public LatLon(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        // Haversine formula to calculate the distance between two lat/lon points
        public static double CalculateDistance(LatLon point1, LatLon point2)
        {
            double R = 6371.0; // Radius of the Earth in kilometers
            double dLat = DegreeToRadian(point2.Latitude - point1.Latitude);
            double dLon = DegreeToRadian(point2.Longitude - point1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreeToRadian(point1.Latitude)) * Math.Cos(DegreeToRadian(point2.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        public void MoveNorth(double km)
        {
            Latitude += (float)((km / EARTH_RADIUS) * (180 / Math.PI));
        }

        public void MoveSouth(double km)
        {
            Latitude -= (float)((km / EARTH_RADIUS) * (180 / Math.PI));
        }

        public void MoveEast(double km)
        {
            Longitude += (float)((km / EARTH_RADIUS) * (180 / Math.PI) / Math.Cos(DegreeToRadian(Latitude)));
        }

        public void MoveWest(double km)
        {
            Longitude -= (float)((km / EARTH_RADIUS) * (180 / Math.PI) / Math.Cos(DegreeToRadian(Latitude)));
        }

        private static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180.0;
        }
        public static (LatLon, LatLon) FindLongestDistance(LatLon[] latLons)
        {
            LatLon mostWest = latLons[0];
            LatLon mostEast = latLons[0];
            LatLon mostNorth = latLons[0];
            LatLon mostSouth = latLons[0];

            foreach (var point in latLons)
            {
                if (point.Longitude < mostWest.Longitude) mostWest = point;
                if (point.Longitude > mostEast.Longitude) mostEast = point;
                if (point.Latitude > mostNorth.Latitude) mostNorth = point;
                if (point.Latitude < mostSouth.Latitude) mostSouth = point;
            }

            double westEastDistance = LatLon.CalculateDistance(mostWest, mostEast);
            double northSouthDistance = LatLon.CalculateDistance(mostNorth, mostSouth);

            return westEastDistance > northSouthDistance ? (mostWest, mostEast) : (mostNorth, mostSouth);
        }

        public static void FindLongestDistance(LatLon[] latLons, out LatLon adjustedPoint1, out LatLon adjustedPoint2)
        {
            LatLon mostWest = latLons[0];
            LatLon mostEast = latLons[0];
            LatLon mostNorth = latLons[0];
            LatLon mostSouth = latLons[0];

            foreach (var point in latLons)
            {
                if (point.Longitude < mostWest.Longitude) mostWest = point;
                if (point.Longitude > mostEast.Longitude) mostEast = point;
                if (point.Latitude > mostNorth.Latitude) mostNorth = point;
                if (point.Latitude < mostSouth.Latitude) mostSouth = point;
            }

            double westEastDistance = LatLon.CalculateDistance(mostWest, mostEast);
            double northSouthDistance = LatLon.CalculateDistance(mostNorth, mostSouth);

            if (westEastDistance > northSouthDistance)
            {
                adjustedPoint1 = new LatLon(mostWest.Latitude, mostWest.Longitude);
                adjustedPoint2 = new LatLon(mostEast.Latitude, mostEast.Longitude);
                adjustedPoint1.MoveWest(1);
                adjustedPoint2.MoveEast(1);
            }
            else
            {
                adjustedPoint1 = new LatLon(mostNorth.Latitude, mostNorth.Longitude);
                adjustedPoint2 = new LatLon(mostSouth.Latitude, mostSouth.Longitude);
                adjustedPoint1.MoveNorth(1);
                adjustedPoint2.MoveSouth(1);
            }
        }

        public static LatLon CalculateNearestPointOnRectangle(LatLon point, LatLon adjustedPoint1, LatLon adjustedPoint2)
        {
            float nearestLat = point.Latitude;
            float nearestLon = point.Longitude;

            // Adjust latitude
            if (point.Latitude > Math.Max(adjustedPoint1.Latitude, adjustedPoint2.Latitude))
                nearestLat = Math.Max(adjustedPoint1.Latitude, adjustedPoint2.Latitude);
            else if (point.Latitude < Math.Min(adjustedPoint1.Latitude, adjustedPoint2.Latitude))
                nearestLat = Math.Min(adjustedPoint1.Latitude, adjustedPoint2.Latitude);

            // Adjust longitude
            if (point.Longitude < Math.Min(adjustedPoint1.Longitude, adjustedPoint2.Longitude))
                nearestLon = Math.Min(adjustedPoint1.Longitude, adjustedPoint2.Longitude);
            else if (point.Longitude > Math.Max(adjustedPoint1.Longitude, adjustedPoint2.Longitude))
                nearestLon = Math.Max(adjustedPoint1.Longitude, adjustedPoint2.Longitude);

            return new LatLon(nearestLat, nearestLon);
        }

        public static Localization ReturnPredictedPoint(LatLon[] latLons)
        {
            LatLon point1, point2;
            FindLongestDistance(latLons, out point1, out point2);
            var latlon =  CalculateNearestPointOnRectangle(latLons[0], point1, point2);
            var localization = new Localization()
            {
                Latitude = latlon.Latitude,
                Longitude = latlon.Longitude
            };
            return localization;
        }

    }
}
