using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.Model.Dao;
using ShoeShopOnline.WebApp.Common;
using ShoeShopOnline.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShoeShopOnline.WebApp.Controllers
{
    public class CartController : BaseController
    {
        ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(Guid? productId, int quantity)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var product = db.Product.Find(productId);
                var cart = Session[CartSession];
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;

                    if (list.Exists(x => x.Product.Id == productId))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.Id == productId)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        //Them moi doi tuong cart item
                        var item = new CartItem();
                        item.Product = product;
                        item.Quantity = quantity;
                        list.Add(item);
                    }
                    //gan vao session
                    Session[CartSession] = list;
                    SetAlert("Thêm vào giỏ thành công", "success");
                }
                else
                {
                    //Them moi doi tuong cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);
                    //Gan item vao session
                    Session[CartSession] = list;
                    SetAlert("Thêm vào giỏ thành công", "success");
                }
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Bạn phải đăng nhập để có thể mua hàng", "warning");
                return RedirectToAction("Login", "User");
            }

        }

        // Update cart
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                if (jsonItem.Quantity < 1)
                {
                    SetAlert("Số lượng phải lớn hơn 0", "warning");
                }
                else if (jsonItem != null && jsonItem.Quantity <= item.Product.Quantity)
                {
                    item.Quantity = jsonItem.Quantity;
                    Session[CartSession] = sessionCart;
                    SetAlert("Cập nhật giỏ hàng thành công", "success");
                }
                else
                {
                    SetAlert("Không đủ sản phẩm để bán", "warning");
                }
            }
            return Json(new
            {
                status = true
            });
        }
        // Delete All item in cart
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            SetAlert("Bạn đã hủy tất cả sản phẩm", "warning");

            return Json(new
            {
                status = true
            });
        }

        // Delete element in cart
        public JsonResult Delete(Guid? id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.Id == id);
            Session[CartSession] = sessionCart;
            SetAlert("Bạn đã hủy một sản phẩm", "warning");
            return Json(new
            {
                status = true
            });
        }

        // Payment for item
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string mobile)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var order = new Order();
            order.UserId = session.UserId;
            order.CreatedOn = DateTime.Now;
            order.ShipAddress = session.Address;
            order.ShipPhone = mobile;
            order.ShipName = session.FullName;
            order.ShipEmail = session.Email;
            order.Status = false;
            order.CreatedOn = DateTime.Now;
            order.CreatedBy = session.UserName;
            order.IsDeleted = false;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailsDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductId = item.Product.Id;
                    orderDetail.OrderId = id;
                    if (item.Product.PromotionPrice != null)
                    {
                        orderDetail.Price = item.Product.PromotionPrice;
                        total += (item.Product.PromotionPrice.GetValueOrDefault(0) * item.Quantity);
                    }
                    else
                    {
                        orderDetail.Price = item.Product.Price;
                        total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    }
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.CreatedOn = DateTime.Now;
                    orderDetail.CreatedBy = session.UserName;
                    orderDetail.IsDeleted = false;
                    detailDao.Insert(orderDetail);

                    //Sub quantiy in Product table
                    var product = new ProductDao();
                    product.setQuantity(orderDetail.ProductId, orderDetail.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Client/neworder.html"));

                content = content.Replace("{{CustomerName}}", session.FullName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", session.Email);
                content = content.Replace("{{Address}}", session.Address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(session.Email, "Đơn hàng mới từ Free Style Shoe", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Free Style Shoe", content);
            }
            catch (Exception)
            {
                //ghi log
                SetAlert("Lỗi!", "error");
            }
            Session.Remove(CartSession);
            SetAlert("Bạn vừa mua hàng thành công. Nhân viên của chúng tối sẽ liên hệ với bạn trong vài phút để xác nhận đơn hàng.", "success");
            return Redirect("/gio-hang");
        }
    }
}