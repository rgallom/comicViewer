using Comics.Models;
using Comics.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Comics.Controllers
{
    public class HomeController : Controller
    {
        ComicService comicService;

        public HomeController()
        {
            this.comicService = new ComicService();
        }
        public async Task<ActionResult> Index()
        {
            var comic = await comicService.GetCurrentComic();
            return View(comic);
       
        }
    }
}