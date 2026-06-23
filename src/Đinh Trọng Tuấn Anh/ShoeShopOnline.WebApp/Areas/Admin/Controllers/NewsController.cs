using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/News
        public ActionResult Index(string sortOrder, int? page, string searchString, string currentFilter)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            //Sort
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.NewsCategorySortParm = sortOrder == "NewsCategory" ? "newsCategory_desc" : "NewsCategory";
            var news = db.News.Include(n => n.NewsCategory).Where(x => x.IsDeleted == false);

            //Filter
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.Name.Contains(searchString) || s.NewsCategory.Name.Contains(searchString)
                || s.Summary.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    news = news.OrderByDescending(s => s.Name);
                    break;
                case "NewsCategory":
                    news = news.OrderBy(s => s.NewsCategory.Name);
                    break;
                case "newsCategory_desc":
                    news = news.OrderByDescending(s => s.NewsCategory.Name);
                    break;
                default:
                    news = news.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(news.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(Guid? id)
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

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.NewsCategoryId = new SelectList(db.NewsCategory, "Id", "Name");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                news.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(news);
                news.CreatedBy = session.UserName;
                news.Id = Guid.NewGuid();
                db.News.Add(news);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/tin-tuc");
            }

            ViewBag.NewsCategoryId = new SelectList(db.NewsCategory, "Id", "Name", news.NewsCategoryId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsCategoryId = new SelectList(db.NewsCategory, "Id", "Name", news.NewsCategoryId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(news);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                news.ModifiedBy = session.UserName;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/tin-tuc");
            }
            ViewBag.NewsCategoryId = new SelectList(db.NewsCategory, "Id", "Name", news.NewsCategoryId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            News news = db.News.Find(id);
            news.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/tin-tuc");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
