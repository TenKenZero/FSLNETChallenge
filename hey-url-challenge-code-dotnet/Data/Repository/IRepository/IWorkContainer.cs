using System;

namespace hey_url_challenge_code_dotnet.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable
    {
        IUrlRepository Url { get; }

        IClickRepository Click { get; }

        void Save();
    }
}
