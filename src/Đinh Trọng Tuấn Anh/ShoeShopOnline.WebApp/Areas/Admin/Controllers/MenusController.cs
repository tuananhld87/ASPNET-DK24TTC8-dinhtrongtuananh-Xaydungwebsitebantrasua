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
    public class MenusController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Menus
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            return View(db.Menu.Where(x => x.IsDeleted == false).ToList());
        }

        // GET: Admin/Menus/Details/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menus/Create
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.InsertAuditFields(menu);
                menu.CreatedBy = session.UserName;
                db.Menu.Add(menu);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/menu");
            }

            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(int? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                AuditTable.UpdateAuditFields(menu);
                menu.ModifiedBy = session.UserName;
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/menu");
            }
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            menu.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/menu");
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
