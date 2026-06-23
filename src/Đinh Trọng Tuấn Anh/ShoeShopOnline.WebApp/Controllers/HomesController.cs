using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.Model.Dao;
using ShoeShopOnline.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShoeShopOnline.WebApp.Controllers
{
    public class HomesController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        private const string CartSession = "CartSession";

        // GET: Homes
        public ActionResult Index()
        {
            return View();
        }
        // Footer
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var result = db.Footer.SingleOrDefault(x => x.IsDeleted == false);
            return PartialView(result);
        }
        //Main menu
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            List<Menu> dsmenu = new List<Menu>();
            dsmenu = db.Menu.Where(x => x.ParentId == null && x.IsDeleted == false).OrderBy(x => x.DisplayOrder).ToList();
            return PartialView(dsmenu);
        }
        [ChildActionOnly]
        public ActionResult SubMenu(int ParentId)
        {
            List<Menu> lst = new List<Menu>();
            lst = db.Menu.Where(x => x.ParentId == ParentId && x.IsDeleted == false).OrderBy(x => x.DisplayOrder).ToList();
            ViewBag.Count = lst.Count();
            return PartialView(lst);
        }
        //Slide
        [ChildActionOnly]
        public PartialViewResult Slide()
        {
            List<Slide> getSlide = new List<Slide>();
            getSlide = db.Slide.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Count = getSlide.Count();
            return PartialView(getSlide);
        }
        //Footer -- Contact
        [ChildActionOnly]
        public ActionResult Contact()
        {
            List<Contact> list = new List<Contact>();
            list = db.Contact.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Count = list.Count();
            return PartialView(list);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        //Cancel payment - order for customer
        public ActionResult CancelOrder(Guid? id)
        {
            Order order = db.Order.Find(id);
            if (order.Status == true)
            {
                SetAlert("Đơn hàng đã được vận chuyển, bạn không thể hủy!","warning");
            }
            else
            {
                order.IsDeleted = true;
                OrderDetail orderDetail = db.OrderDetail.FirstOrDefault(x => x.OrderId == order.Id);
                Product product = db.Product.FirstOrDefault(x => x.Id == orderDetail.ProductId);
                if (orderDetail != null)
                {
                    orderDetail.IsDeleted = true;
                }
                if (product != null)
                {
                    product.Quantity += orderDetail.Quantity;
                }
                db.SaveChanges();
                SetAlert("Bạn vừa hủy một đơn hàng", "warning");
            }
            return Redirect("/lich-su-mua-hang");
        }
        //Payment history
        public ActionResult HistoryPayment()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                return View(db.Order.Where(x => x.UserId == session.UserId && x.IsDeleted == false).ToList());
            }
            else
            {
                SetAlert("Bạn cần đăng nhập để thực hiện", "warning");
                return Redirect("/dang-nhap");
            }
        }
        public ActionResult HistoryPaymentDetail(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = new OrderDetail();
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(db.OrderDetail.Where(x => x.OrderId == order.Id).OrderByDescending(x => x.CreatedOn).ToList());
        }
        //Cancel order
        public ActionResult CancelOrderView()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session != null)
            {
                return View(db.Order.Where(x => x.UserId == session.UserId && x.IsDeleted == true).ToList());
            }
            else
            {
                SetAlert("Bạn cần đăng nhập để thực hiện", "warning");
                return Redirect("/dang-nhap");
            }
        }
        //Undo order
        public ActionResult UndoOrder(Guid? id)
        {
            Order order = db.Order.Find(id);
            order.IsDeleted = false;
            //Undo in order detail and Add turn quantity of product
            OrderDetail orderDetail = db.OrderDetail.FirstOrDefault(x => x.OrderId == order.Id);
            Product product = db.Product.FirstOrDefault(x => x.Id == orderDetail.ProductId);
            if (orderDetail != null)
            {
                orderDetail.IsDeleted = false;
            }
            if (product != null)
            {
                product.Quantity -= orderDetail.Quantity;
            }
            db.SaveChanges();
            SetAlert("Phục hồi đơn hàng thành công", "success");
            return Redirect("/lich-su-mua-hang");
        }
    }
}