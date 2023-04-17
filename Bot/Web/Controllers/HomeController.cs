using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DrVegapunk.Bot.Modules;

namespace DrVegapunk.Bot.Controllers
{
    // TODO: Improve
    public class HomeController : Controller
    {
        private const string _indexViewPath = @"~/Web/Views/Home/Index.cshtml";
        private readonly ChatGPTModule _chatGPT;
        private readonly DallEModule _dallE;

        public HomeController(
            ChatGPTModule chatGPT,
            DallEModule dallE)
        {
            _chatGPT = chatGPT;
            _dallE = dallE;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View(_indexViewPath);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View(_indexViewPath);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View(_indexViewPath);
        }

        // POST: HomeController/ResetUserAttempts
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetUserAttempts()
        {
            _chatGPT.ResetAttempts();
            _dallE.ResetAttempts();
            return View(_indexViewPath);
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_indexViewPath);
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_indexViewPath);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_indexViewPath);
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_indexViewPath);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_indexViewPath);
            }
        }
    }
}
