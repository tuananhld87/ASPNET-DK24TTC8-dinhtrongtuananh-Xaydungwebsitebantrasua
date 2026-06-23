using ShoeShopOnline.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShopOnline.WebApp.Controllers
{
    public class AboutController : Controller
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        // GET: About
        public ActionResult Index()
        { 
            return View(db.About.Where(x => x.IsDeleted == false).ToList());
        }
    }
}