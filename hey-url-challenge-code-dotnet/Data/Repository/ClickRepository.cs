using hey_url_challenge_code_dotnet.Data.Repository.IRepository;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Data.Repository
{
    public class ClickRepository : Repository<Click>, IClickRepository
    {
        private readonly ApplicationContext _db;       
        public ClickRepository(ApplicationContext db) : base(db) 
        {
            _db = db;            
        }
        public Click Create(string url, string browser, string os)
        {
            WorkContainer workContainer = new WorkContainer(_db);
            Click newClick = new Click();
            newClick.ID = Guid.NewGuid();
            newClick.Platform = os;
            newClick.Browser = browser;
            newClick.Date = DateTime.Now.ToString();
            newClick.Url = workContainer.Url.GetUrlByShort(url);
            newClick.Url.Count += 1;

            Add(newClick);
            workContainer.Url.Update(newClick.Url);
            _db.SaveChanges();

            return newClick;
        }

        public Dictionary<string, int> GetBrowseClicks(string url)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            var objUrl = _db.Urls.FirstOrDefault(i => i.ShortUrl == url);
            var objClick = _db.Clicks
                .Where(i => i.UrlID == objUrl.ID)
                .GroupBy(i => i.Browser)
                .Select(g => new { browser = g.Key, count = g.Count() });

            foreach (var item in objClick)
            {
                map.Add(item.browser, item.count);
            }

            return map;
        }

        public Dictionary<string, int> GetDailyClicks(string url)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            var objUrl = _db.Urls.FirstOrDefault(i => i.ShortUrl == url);
            var objClick = _db.Clicks
                .Where(i => i.UrlID == objUrl.ID)
                .GroupBy(i => i.Date.Substring(0,10))
                .Select(g => new { date = g.Key, count = g.Count() });

            foreach( var item in objClick)
            {
                map.Add(item.date, item.count);
            }

            return map;
        }

        public Dictionary<string, int> GetPlatformClicks(string url)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            var objUrl = _db.Urls.FirstOrDefault(i => i.ShortUrl == url);
            var objClick = _db.Clicks
                .Where(i => i.UrlID == objUrl.ID)
                .GroupBy(i => i.Platform)
                .Select(g => new { platform = g.Key, count = g.Count() });

            foreach (var item in objClick)
            {
                map.Add(item.platform, item.count);
            }

            return map;
        }

        public void Update(Click click)
        {
            var objClick = _db.Clicks.FirstOrDefault(i => i.ID == click.ID);
            objClick.Platform = click.Platform;
            objClick.Browser = click.Browser;
            objClick.Date= click.Date;
            objClick.UrlID = click.UrlID;
        }
    }
}
