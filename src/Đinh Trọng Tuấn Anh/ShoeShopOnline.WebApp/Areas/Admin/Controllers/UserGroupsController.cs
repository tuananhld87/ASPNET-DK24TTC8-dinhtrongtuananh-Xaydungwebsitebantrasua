using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class UserGroupsController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/UserGroups
        [HasCredential(RoleId = "VIEW_USER_GROUP")]
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.UserGroup.ToList());
        }

        // GET: Admin/UserGroups/Details/5
        [HasCredential(RoleId = "VIEW_USER_GROUP")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // GET: Admin/UserGroups/Create
        [HasCredential(RoleId = "CREATE_USER_GROUP")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        // POST: Admin/UserGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "CREATE_USER_GROUP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                db.UserGroup.Add(userGroup);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/nhom-nguoi-dung");
            }

            return View(userGroup);
        }

        // GET: Admin/UserGroups/Edit/5
        [HasCredential(RoleId = "EDIT_USER_GROUP")]
        public ActionResult Edit(string id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // POST: Admin/UserGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "EDIT_USER_GROUP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGroup).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/nhom-nguoi-dung");
            }
            return View(userGroup);
        }

        // GET: Admin/UserGroups/Delete/5
        [HasCredential(RoleId = "DELETE_USER_GROUP")]
        public ActionResult Delete(string id)
        {
            var existCredential = db.Credentials.Where(x => x.UserGroupId == id);
            var existUser = db.User.Where(x => x.GroupId == id);
            if (existCredential != null || existUser != null)
            {
                return PartialView("_Delete");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGroup userGroup = db.UserGroup.Find(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // POST: Admin/UserGroups/Delete/5
        [HasCredential(RoleId = "DELETE_USER_GROUP")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserGroup userGroup = db.UserGroup.Find(id);
            db.UserGroup.Remove(userGroup);
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/nhom-nguoi-dung");
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
