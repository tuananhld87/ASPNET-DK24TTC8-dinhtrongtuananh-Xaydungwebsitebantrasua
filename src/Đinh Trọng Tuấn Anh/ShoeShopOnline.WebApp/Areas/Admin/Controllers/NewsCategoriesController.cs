using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class NewsCategoriesController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/NewsCategories
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.NewsCategory.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList());
        }

        // GET: Admin/NewsCategories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // GET: Admin/NewsCategories/Create
        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View();
        }

        // POST: Admin/NewsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                newsCategory.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(newsCategory);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                newsCategory.CreatedBy = session.UserName;
                newsCategory.Id = Guid.NewGuid();
                db.NewsCategory.Add(newsCategory);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/loai-tin-tuc");
            }

            return View(newsCategory);
        }

        // GET: Admin/NewsCategories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: Admin/NewsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(newsCategory);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                newsCategory.ModifiedBy = session.UserName;
                db.Entry(newsCategory).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/loai-tin-tuc");
            }
            return View(newsCategory);
        }

        // GET: Admin/NewsCategories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var isExists = db.News.Where(x => x.NewsCategoryId == id);
            if (isExists != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            if (newsCategory == null)
            {
                return HttpNotFound();
            }
            return View(newsCategory);
        }

        // POST: Admin/NewsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            NewsCategory newsCategory = db.NewsCategory.Find(id);
            newsCategory.IsDeleted = true;
            db.SaveChanges();
            return Redirect("/quan-tri/loai-tin-tuc");
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
