using hey_url_challenge_code_dotnet.Models;

namespace hey_url_challenge_code_dotnet.Data.Repository.IRepository
{
    public interface IUrlRepository : IRepository<Url>
    {
        Url Create(string fullUrl);

        Url GetUrlByShort(string shortUrl);
        void Update(Url url);
        string generateShortUrl();
    }
}
