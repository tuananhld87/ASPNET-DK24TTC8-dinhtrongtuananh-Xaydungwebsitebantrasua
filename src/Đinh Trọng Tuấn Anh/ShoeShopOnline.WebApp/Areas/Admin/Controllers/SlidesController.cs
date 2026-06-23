using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class SlidesController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Slides
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Slide.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).Take(3).ToList());
        }

        // GET: Admin/Slides/Details/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Admin/Slides/Create
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create([Bind(Include = "Id,UrlImage,DisplayOrder,Link,Description,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.InsertAuditFields(slide);
                slide.CreatedBy = session.UserName;
                db.Slide.Add(slide);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/slide");
            }

            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(int? id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit([Bind(Include = "Id,UrlImage,DisplayOrder,Link,Description,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.UpdateAuditFields(slide);
                slide.ModifiedBy = session.UserName;
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/slide");
            }
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slide.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slide.Find(id);
            slide.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/slide");
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
