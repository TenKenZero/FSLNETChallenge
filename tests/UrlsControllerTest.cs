using hey_url_challenge_code_dotnet.Data.Repository;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace tests
{
    [TestFixture]
    public class UrlsControllerTest
    {
        private ApplicationContext context;
        private WorkContainer workContainer;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer("Server=LC-WORKLP;Database=NETchallenge;User ID=sa;Password=1234;Trusted_Connection=true;Encrypt=false;MultipleActiveResultSets=true;Trust Server Certificate=true")
                .Options;
            context = new ApplicationContext(options);
            workContainer = new WorkContainer(context);
        }

        [Test]
        public void TestShortUrl()
        {
            //Operations
            string shortUrl = workContainer.Url.generateShortUrl();

            //Validations
            //Should return a 5 characters string
            Assert.AreEqual(5, shortUrl.Length);
        }

        [Test]
        public void TestUrlCreation()
        {
            //Data
            string fullUrl = "http://www.google.com";

            //Operations
            Url newUrl = workContainer.Url.Create(fullUrl);

            //validations
            //The new Url object's should be an object of the Url class
            Assert.That(newUrl, Is.InstanceOf<Url>());
            //The new Url object's FullUrl should be the same as the variable fullUrl
            Assert.AreEqual(fullUrl, newUrl.FullUrl);
            //The new Url object's ShortUrl should return a 5 characters string
            Assert.AreEqual(5, newUrl.ShortUrl.Length);
        }

        [Test]
        public void TestClickCreation()
        {
            //Data
            string OS = "Linux";
            string browser = "Firefox";
            string fullUrl = "http://www.google.com";

            //Operations
            Url url = workContainer.Url.Create(fullUrl);
            Click click = workContainer.Click.Create(url.ShortUrl, browser, OS);

            //Validations
            //The new Url object's should be an object of the Url class
            Assert.That(url, Is.InstanceOf<Url>());
            //The new Click object's should be an object of the Click class
            Assert.That(click, Is.InstanceOf<Click>());
            //The new Url object's FullUrl should be the same as the variable fullUrl
            Assert.AreEqual(fullUrl, click.Url.FullUrl);
            //The new Url object's ShortUrl should return a 5 characters string
            Assert.AreEqual(5, click.Url.ShortUrl.Length);
        }
    }
}