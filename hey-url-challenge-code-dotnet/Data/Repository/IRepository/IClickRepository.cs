using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;

namespace hey_url_challenge_code_dotnet.Data.Repository.IRepository
{
    public interface IClickRepository : IRepository<Click>
    {
        Click Create(string url, string browser, string os);
        void Update(Click click);

        Dictionary<string, int> GetDailyClicks(string url);
        Dictionary<string, int> GetBrowseClicks(string url);
        Dictionary<string, int> GetPlatformClicks(string url);
    }
}
