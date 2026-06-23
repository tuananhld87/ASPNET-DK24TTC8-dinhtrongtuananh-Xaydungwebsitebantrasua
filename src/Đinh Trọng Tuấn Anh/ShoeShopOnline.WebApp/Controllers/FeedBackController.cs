using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShoeShopOnline.WebApp.Controllers
{
    public class FeedBackController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        // GET: Contact
        [HttpGet]
        public ActionResult ContactFromCustomer()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                return View();
            }
            else
            {
                SetAlert("Bạn cần đăng nhập để thực hiện", "warning");
                return Redirect("/dang-nhap");
            }
        }
        [HttpPost]
        public ActionResult ContactFromCustomer(Feedback model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(model);
                db.Feedback.Add(model);
                db.SaveChanges();
                SetAlert("Cảm ơn bạn đã góp ý", "success");
                return Redirect("/phan-hoi-y-kien-khach-hang");
            }
            return View(model);
        }
        // Phan hoi y kien khach hang
        public ActionResult ReplyFeedBack()
        {
            var result = db.Feedback.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList();
            return View(result);
        }
    }
}