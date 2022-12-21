using hey_url_challenge_code_dotnet.Data.Repository.IRepository;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using System;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Data.Repository
{
    public class UrlRepository : Repository<Url>, IUrlRepository
    {
        private readonly ApplicationContext _db;
        private static Random random = new Random();
        public UrlRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
        public Url Create(string fullUrl)
        {
            Url newUrl = new Url();
            newUrl.ID = Guid.NewGuid();
            newUrl.FullUrl = fullUrl;
            newUrl.ShortUrl = generateShortUrl();
            newUrl.DateCreation = DateTime.Now.ToString();
            newUrl.Count = 0;
            Add(newUrl);
            _db.SaveChanges();

            return newUrl;
        }        

        public string generateShortUrl()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(element: chars, count:5)
                .Select(c => c[random.Next(c.Length)]).ToArray()
                );
        }

        public void Update(Url url)
        {
            var objUrl = _db.Urls.FirstOrDefault(s => s.ID == url.ID);
            objUrl.FullUrl = url.FullUrl;
            objUrl.ShortUrl = url.ShortUrl;
            objUrl.Count = url.Count;

            _db.SaveChanges();
        }

        public Url GetUrlByShort(string shortUrl)
        {
            return _db.Urls.FirstOrDefault(s => s.ShortUrl == shortUrl);
        }
    }
}
