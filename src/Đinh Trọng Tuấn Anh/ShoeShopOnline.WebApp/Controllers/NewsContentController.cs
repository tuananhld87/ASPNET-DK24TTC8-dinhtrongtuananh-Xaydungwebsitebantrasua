using PagedList;
using ShoeShopOnline.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShoeShopOnline.WebApp.Controllers
{
    public class NewsContentController : Controller
    {
        // GET: News
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        public ActionResult LoadAllNews(int? page)
        {
            List<News> news = new List<News>();
            news = db.News.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList();
            ViewBag.Count = news.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NewsDetails(Guid? id) // Sử dụng Guid? để xử lý giá trị null
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        public ActionResult LoadNewsCategory()
        {
            List<NewsCategory> newsCategories = new List<NewsCategory>();
            newsCategories = db.NewsCategory.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Count = newsCategories.Count();
            return PartialView(newsCategories);
        }

        public ActionResult NewsCategoryDetail(NewsCategory newsCategory)
        {
            List<News> list = new List<News>();
            list = db.News.Where(x => x.IsDeleted == false && x.NewsCategoryId == newsCategory.Id).ToList();
            ViewBag.Count = list.Count();
            return View(list);
        }
    }
}
