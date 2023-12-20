using Sirena.Api.Domain.Common;

namespace Sirena.Api.Domain
{
    public class Airport
    {
        public IataCode IataCode { get; set; }
        public Name Name { get; set; }
        public Longitude Longitude { get; set; }
        public Latitude Latitude { get; set; }
    }
}
