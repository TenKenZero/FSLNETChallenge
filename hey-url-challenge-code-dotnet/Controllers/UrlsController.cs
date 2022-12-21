using System;
using System.Collections.Generic;
using hey_url_challenge_code_dotnet.Data.Repository;
using hey_url_challenge_code_dotnet.Data.Repository.IRepository;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        HomeViewModel model = new HomeViewModel();
        private readonly IWorkContainer _workContainer;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IWorkContainer workContainer)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _workContainer = workContainer;
        }

        public IActionResult Index()
        {
            model.Urls = _workContainer.Url.GetAll();

            ViewBag.homeUrl = $"{Request.Scheme}://{Request.Host}";
            model.NewUrl = new();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string fullUrl)
        {
            model.NewUrl = _workContainer.Url.Create(fullUrl);
            if (model.NewUrl == null)
            {
                throw new NullReferenceException("problem with the creation of the Url");
            }

            return RedirectToAction("Index");
        }

        [Route("/{url}")]
        public IActionResult Visit(string url) 
        {
            model.NewClick = _workContainer.Click.Create(url, this.browserDetector.Browser.Name, this.browserDetector.Browser.OS);
            if (model.NewClick == null)
            {
                throw new NullReferenceException("problem with the creation of the Click");
            }

            return new OkObjectResult($"{url}, {this.browserDetector.Browser.OS}, {this.browserDetector.Browser.Name}");
        }

        [Route("urls/{url}")]
        public IActionResult Show(string url) => View(new ShowViewModel
        {
            Url = _workContainer.Url.GetUrlByShort(url),
            DailyClicks = _workContainer.Click.GetDailyClicks(url),
            BrowseClicks = _workContainer.Click.GetBrowseClicks(url),
            PlatformClicks = _workContainer.Click.GetPlatformClicks(url),
        });
    }
}