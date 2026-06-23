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
    public class FeedbacksController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Feedbacks
        public ActionResult Index()
        {
            CountMessage();
            CountProduct();
            CountOrder();
            var result = db.Feedback.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn);
            return View(result.ToList());
        }

        // GET: Admin/Contacts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Admin/Feedbacks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Admin/Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Email,Address,Content,Reply,CreatedOn,CreatedBy,IsDeleted")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(feedback);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                feedback.ModifiedBy = session.UserName;
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/phan-hoi-y-kien-khach-hang");
            }
            return View(feedback);
        }

        // GET: Admin/Feedbacks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            CountMessage();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Admin/Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Feedback feedback = db.Feedback.Find(id);
            feedback.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/phan-hoi-y-kien-khach-hang");
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
