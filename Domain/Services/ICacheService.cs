namespace Sirena.Api.Domain.Services
{
    public interface ICacheService
    {
 
        bool Contains(string code);
        Airport Get(string code);
        void Add(Airport airport);
    }
}
