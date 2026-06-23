using System;
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
    public class FootersController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Footers
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.Footer.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList());
        }

        // GET: Admin/Footers/Details/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Footer.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // GET: Admin/Footers/Create
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        // POST: Admin/Footers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create([Bind(Include = "Id,CoppyRight,Address,PhoneNumber,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Footer footer)
        {
            if (ModelState.IsValid)
            {
                AuditTable.InsertAuditFields(footer);
                footer.Id = Guid.NewGuid();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                footer.CreatedBy = session.UserName;
                db.Footer.Add(footer);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/footer");
            }
            return View(footer);
        }

        // GET: Admin/Footers/Edit/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Footer.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: Admin/Footers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit([Bind(Include = "Id,CoppyRight,Address,PhoneNumber,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Footer footer)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(footer);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                footer.ModifiedBy = session.UserName;
                db.Entry(footer).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/footer");
            }
            return View(footer);
        }

        // GET: Admin/Footers/Delete/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Footer.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: Admin/Footers/Delete/5
        [HttpPost, ActionName("Delete")]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Footer footer = db.Footer.Find(id);
            //db.Footer.Remove(footer);
            footer.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/footer");
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
