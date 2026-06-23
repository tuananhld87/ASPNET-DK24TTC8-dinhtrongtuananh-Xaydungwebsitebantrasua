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
    public class ContactsController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Contacts
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View(db.Contact.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).Take(2).ToList());
        }

        // GET: Admin/Contacts/Details/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Create
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            return View();
        }

        // POST: Admin/Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ObjectName,Address,Mobile,Website,Email,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsDeleted")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                AuditTable.InsertAuditFields(contact);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                contact.CreatedBy = session.UserName;
                db.Contact.Add(contact);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/thong-tin-cua-hang");
            }

            return View(contact);
        }

        // GET: Admin/Contacts/Edit/5
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
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(contact);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                contact.ModifiedBy = session.UserName;
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/thong-tin-cua-hang");
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Delete/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Contacts/Delete/5
        [HasCredential(RoleId = "VIEW_LAYOUT")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contact.Find(id);
            contact.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/thong-tin-cua-hang");
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
