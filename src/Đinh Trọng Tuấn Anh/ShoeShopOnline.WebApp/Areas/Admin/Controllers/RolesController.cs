using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class RolesController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Roles
        [HasCredential(RoleId = "VIEW_ROLE")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Role.ToList());
        }

        // GET: Admin/Roles/Details/5
        [HasCredential(RoleId = "VIEW_ROLE")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Admin/Roles/Create
        [HasCredential(RoleId = "CREATE_ROLE")]
        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "CREATE_ROLE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Role.Add(role);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/quan-ly-phan-quyen");
            }

            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        [HasCredential(RoleId = "EDIT_ROLE")]
        public ActionResult Edit(string id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "EDIT_ROLE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/quan-ly-phan-quyen");
            }
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        [HasCredential(RoleId = "DELETE_ROLE")]
        public ActionResult Delete(string id)
        {
            var existCredential = db.Credentials.Where(x => x.RoleId == id).FirstOrDefault();
            if (existCredential != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HasCredential(RoleId = "DELETE_ROLE")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Role role = db.Role.Find(id);
            db.Role.Remove(role);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/quan-ly-phan-quyen");
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
