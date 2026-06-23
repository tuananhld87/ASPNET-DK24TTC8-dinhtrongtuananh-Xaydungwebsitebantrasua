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
    public class CredentialsController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Credentials
        [HasCredential(RoleId = "VIEW_CREDENTIAL")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            var credentials = db.Credentials.Include(c => c.Role).Include(c => c.UserGroup);
            return View(credentials.ToList());
        }

        // GET: Admin/Credentials/Details/5
        [HasCredential(RoleId = "VIEW_CREDENTIAL")]
        public ActionResult Details(string groupId, string roleId)
        {
            Credential credential = db.Credentials.Find(groupId, roleId);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        // GET: Admin/Credentials/Create
        [HasCredential(RoleId = "CREATE_CREDENTIAL")]
        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            ViewBag.RoleId = new SelectList(db.Role, "Id", "Name");
            ViewBag.UserGroupId = new SelectList(db.UserGroup, "Id", "Name");
            return View();
        }

        // POST: Admin/Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "CREATE_CREDENTIAL")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserGroupId,RoleId")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                db.Credentials.Add(credential);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/phan-quyen-nguoi-dung");
            }

            ViewBag.RoleId = new SelectList(db.Role, "Id", "Name", credential.RoleId);
            ViewBag.UserGroupId = new SelectList(db.UserGroup, "Id", "Name", credential.UserGroupId);
            return View(credential);
        }

        // GET: Admin/Credentials/Delete/5
        [HasCredential(RoleId = "DELETE_CREDENTIAL")]
        public ActionResult Delete(string groupId, string roleId)
        {
            if (groupId == null || roleId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credential credential = db.Credentials.Find(groupId, roleId);
            if (credential == null)
            {
                return HttpNotFound();
            }
            return View(credential);
        }

        // POST: Admin/Credentials/Delete/5
        [HasCredential(RoleId = "DELETE_CREDENTIAL")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string groupId, string roleId)
        {
            Credential credential = db.Credentials.Find(groupId, roleId);
            db.Credentials.Remove(credential);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/phan-quyen-nguoi-dung");
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
