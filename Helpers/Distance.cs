using Sirena.Api.Domain;
using System;

namespace Sirena.Api.Helpers
{
    public class Distance
    {

        public static double CalculateMiles(Airport origin, Airport destination)
        {            
            return CalculateKilometers(origin,destination)/ 1.609;
        }
        public static double CalculateKilometers(Airport origin, Airport destination)
        {

            return Math.Acos((Math.Sin(ConvertDegreesToRadians(origin.Latitude.Value)) * Math.Sin(ConvertDegreesToRadians(destination.Latitude.Value))) + 
                (Math.Cos(ConvertDegreesToRadians(origin.Latitude.Value)) * Math.Cos(ConvertDegreesToRadians(destination.Latitude.Value))) * 
                (Math.Cos(ConvertDegreesToRadians(destination.Longitude.Value) - ConvertDegreesToRadians(origin.Longitude.Value)))) * 6371;
        }
        static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
    }
}
