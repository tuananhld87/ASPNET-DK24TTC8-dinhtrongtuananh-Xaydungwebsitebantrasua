using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.Model.Dao;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Users
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Index(string sortOrder, string currentFilter, int? page, string searchString)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            var user = db.User.Include(u => u.UserGroup);

            //Search
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
                user = user.Where(s => s.UserName.Contains(searchString) || s.Email.ToString().Contains(searchString)
                || s.UserGroup.Name.Contains(searchString) || s.Address.Contains(searchString));
            }
            //Sort
            switch (sortOrder)
            {
                case "username_desc":
                    user = user.OrderByDescending(s => s.UserName);
                    break;
                case "LastName":
                    user = user.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    user = user.OrderByDescending(s => s.LastName);
                    break;
                default:
                    user = user.OrderBy(s => s.UserName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(user.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Users/Details/5
        [HasCredential(RoleId = "VIEW_USER")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        [HasCredential(RoleId = "ADD_USER")]
        public ActionResult Create()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "ADD_USER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckEmail(user.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại trong hệ thống");
                }
                else if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại trong hệ thống");
                }
                else
                {
                    user.Id = Guid.NewGuid();
                    AuditTable.InsertAuditFields(user);
                    var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                    user.CreatedBy = session.UserName;
                    user.Password = Encryptor.MD5Hash(user.Password);
                    user.ConfirmPassword = Encryptor.MD5Hash(user.ConfirmPassword);
                    db.User.Add(user);
                    db.SaveChanges();
                    SetAlert("Thêm mới thành công", "success");
                    return Redirect("/quan-tri/tai-khoan-nguoi-dung");
                }
            }
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name", user.GroupId);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        //[HasCredential(RoleId = "EDIT_USER")]
        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name", user.GroupId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HasCredential(RoleId = "EDIT_USER")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(user);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                user.ModifiedBy = session.UserName;
                user.Password = Encryptor.MD5Hash(user.Password);
                user.ConfirmPassword = Encryptor.MD5Hash(user.ConfirmPassword);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/tai-khoan-nguoi-dung");
            }
            ViewBag.GroupId = new SelectList(db.UserGroup, "Id", "Name", user.GroupId);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        [HasCredential(RoleId = "DELETE_USER")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HasCredential(RoleId = "DELETE_USER")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.User.Find(id);
            user.IsDeleted = true;
            db.SaveChanges();
            return Redirect("/quan-tri/tai-khoan-nguoi-dung");
        }
        //Change status order
        public JsonResult ChangeStatus(Guid? id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
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
