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
    public class ComicController : Controller
    {
        ComicService comicService;

        public ComicController()
        {
            this.comicService = new ComicService();
        }
        
        public async Task<ActionResult> Index(int id)
        {          
            if (id < 1)
            {
                return RedirectToAction("Index", new { id = 1 });
            }
           
            var comic = await comicService.RequestComicByID(id, false);

            if (comic.num != id)
            {
                return RedirectToAction("Index", new { id = comic.num });
            }
            return View(comic);
        }
        
        [ActionName("Previous")]
        public async Task<ActionResult> ComicPrevious(int id)
        {
            var comic = await comicService.RequestComicByID(id);
            return RedirectToAction("Index", new { id = comic.num });
        }
    }

}
