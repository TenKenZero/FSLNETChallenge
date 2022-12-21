using hey_url_challenge_code_dotnet.Data.Repository.IRepository;
using HeyUrlChallengeCodeDotnet.Data;

namespace hey_url_challenge_code_dotnet.Data.Repository
{
    public class WorkContainer : IWorkContainer
    {
        public IUrlRepository Url {get; private set;}
        public IClickRepository Click { get; private set; }
        private readonly ApplicationContext _db;

        public WorkContainer(ApplicationContext db)
        {
            _db = db;
            Url = new UrlRepository(_db);
            Click = new ClickRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
