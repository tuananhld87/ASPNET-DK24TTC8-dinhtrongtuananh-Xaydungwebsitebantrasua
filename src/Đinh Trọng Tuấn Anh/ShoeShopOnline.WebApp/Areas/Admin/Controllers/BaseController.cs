using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using System.Linq;
using System.Web.Mvc;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null || session.GroupId != CommonConstants.ADMIN_GROUP && session.GroupId != CommonConstants.MOD_GROUP)
            {
                //filterContext.Result = new RedirectToRouteResult(new
                //    RouteValueDictionary(new { controller = "Login", action = "Login", Area = "Admin" }));
                filterContext.Result = Redirect("/quan-tri/dang-nhap");
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        protected void CountMessage()
        {
            var feedbacks = db.Feedback.Where(x => x.IsDeleted == false && x.Reply == null);
            TempData["cms"] = feedbacks.Count().ToString();
        }
        protected void CountProduct()
        {
            var products = db.Product.Where(x => x.IsDeleted == false && x.Quantity < 5);
            TempData["cpd"] = products.Count().ToString();
        }
        protected void CountOrder()
        {
            var orders = db.Order.Where(x => x.Status == false && x.IsDeleted == false);
            TempData["ord"] = orders.Count().ToString();
        }
    }
}