using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoeShopOnline.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditProfile",
                url: "ho-so-nguoi-dung",
                defaults: new { controller = "User", action = "EditProfile", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Forgot Password",
                url: "quen-mat-khau",
                defaults: new { controller = "User", action = "ForgotPassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ContactFromCustomer",
                url: "gop-y",
                defaults: new { controller = "FeedBack", action = "ContactFromCustomer", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ReplyFeedBack",
                url: "phan-hoi-y-kien-khach-hang",
                defaults: new { controller = "FeedBack", action = "ReplyFeedBack", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HomePageUser",
                url: "trang-chu",
                defaults: new { controller = "Homes", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About Shop",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "News Content",
                url: "tin-tuc",
                defaults: new { controller = "NewsContent", action = "LoadAllNews", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "News Content Detail",
                url: "chi-tiet-tin-tuc/{metatitle}/{id}",
                defaults: new { controller = "NewsContent", action = "NewsDetails", id = UrlParameter.Optional }
            );
            //Phan loai tin tuc
            routes.MapRoute(
                name: "NewsContent Category",
                url: "loai-tin-tuc/{metatitle}/{id}",
                defaults: new { controller = "NewsContent", action = "NewsCategoryDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet-san-pham/{metatitle}/{id}",
                defaults: new { controller = "Product", action = "Details", id = UrlParameter.Optional }
            );

            //Phan loai san pham
            routes.MapRoute(
                name: "ProductCategory Detail",
                url: "loai-san-pham/{metatitle}/{id}",
                defaults: new { controller = "Product", action = "CategoryDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "View All Product",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Product", action = "LoadAllProduct", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddCart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
            );

            //History payment
            routes.MapRoute(
                name: "Payment History",
                url: "lich-su-mua-hang",
                defaults: new { controller = "Homes", action = "HistoryPayment", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Payment History Detail",
                url: "chi-tiet-don-hang/{id}",
                defaults: new { controller = "Homes", action = "HistoryPaymentDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Payment History Cancel",
                url: "don-hang-da-huy",
                defaults: new { controller = "Homes", action = "CancelOrderView", id = UrlParameter.Optional }
            );
            ///Route for Admin Areas///
            ///////////////////////////
            routes.MapRoute(
                name: "Admin",
                url: "quan-tri",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Home",
                url: "quan-tri/trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            ///Login in Admin Areas
            routes.MapRoute(
                name: "Admin-Login",
                url: "quan-tri/dang-nhap",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            ///LogOut in Admin Areas
            routes.MapRoute(
                name: "Admin-LogOut",
                url: "quan-tri/dang-xuat",
                defaults: new { controller = "Login", action = "LogOut", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            ///FeedBack for customer
            routes.MapRoute(
                name: "Admin-FeedBack",
                url: "quan-tri/phan-hoi-y-kien-khach-hang",
                defaults: new { controller = "FeedBacks", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-FeedBack-Edit",
                url: "quan-tri/phan-hoi-y-kien-khach-hang/cap-nhat",
                defaults: new { controller = "FeedBacks", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Giao dien nguoi dung
            //Menu
            routes.MapRoute(
                name: "Admin-Menu",
                url: "quan-tri/menu",
                defaults: new { controller = "Menus", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Menu-Create",
                url: "quan-tri/menu/them-moi",
                defaults: new { controller = "Menus", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Menu-Edit",
                url: "quan-tri/menu/cap-nhat",
                defaults: new { controller = "Menus", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Slide
            routes.MapRoute(
                name: "Admin-Slide",
                url: "quan-tri/slide",
                defaults: new { controller = "Slides", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Slide-Create",
                url: "quan-tri/slide/them-moi",
                defaults: new { controller = "Slides", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Slide-Edit",
                url: "quan-tri/slide/cap-nhat",
                defaults: new { controller = "Slides", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Contact
            routes.MapRoute(
                name: "Admin-Contact",
                url: "quan-tri/thong-tin-cua-hang",
                defaults: new { controller = "Contacts", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Contact-Create",
                url: "quan-tri/thong-tin-cua-hang/them-moi",
                defaults: new { controller = "Contacts", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Contact-Edit",
                url: "quan-tri/thong-tin-cua-hang/cap-nhat",
                defaults: new { controller = "Contacts", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Footer
            routes.MapRoute(
                name: "Admin-Footer",
                url: "quan-tri/footer",
                defaults: new { controller = "Footers", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Footer-Create",
                url: "quan-tri/footer/them-moi",
                defaults: new { controller = "Footers", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Footer-Edit",
                url: "quan-tri/footer/cap-nhat",
                defaults: new { controller = "Footers", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Quan tri ban viet
            //NewsCategory
            routes.MapRoute(
                name: "Admin-News-Category",
                url: "quan-tri/loai-tin-tuc",
                defaults: new { controller = "NewsCategories", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-News-Category-Create",
                url: "quan-tri/loai-tin-tuc/them-moi",
                defaults: new { controller = "NewsCategories", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-News-Category-Edit",
                url: "quan-tri/loai-tin-tuc/cap-nhat",
                defaults: new { controller = "NewsCategories", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //News
            routes.MapRoute(
                name: "Admin-News",
                url: "quan-tri/tin-tuc",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-News-Create",
                url: "quan-tri/tin-tuc/them-moi",
                defaults: new { controller = "News", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-News-Edit",
                url: "quan-tri/tin-tuc/cap-nhat",
                defaults: new { controller = "News", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Product
            routes.MapRoute(
                name: "Admin-Product",
                url: "quan-tri/danh-muc-san-pham",
                defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Product-Create",
                url: "quan-tri/danh-muc-san-pham/them-moi",
                defaults: new { controller = "Products", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Product-Edit",
                url: "quan-tri/danh-muc-san-pham/cap-nhat",
                defaults: new { controller = "Products", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //ProductCategory
            routes.MapRoute(
                name: "Admin-ProductCategory",
                url: "quan-tri/loai-san-pham",
                defaults: new { controller = "ProductCategories", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-ProductCategory-Create",
                url: "quan-tri/loai-san-pham/them-moi",
                defaults: new { controller = "ProductCategories", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-ProductCategory-Edit",
                url: "quan-tri/loai-san-pham/cap-nhat",
                defaults: new { controller = "ProductCategories", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //About
            routes.MapRoute(
                name: "Admin-About",
                url: "quan-tri/gioi-thieu-cua-hang",
                defaults: new { controller = "Abouts", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-About-Create",
                url: "quan-tri/gioi-thieu-cua-hang/them-moi",
                defaults: new { controller = "Abouts", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-About-Edit",
                url: "quan-tri/gioi-thieu-cua-hang/cap-nhat",
                defaults: new { controller = "Abouts", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Quan ly don hang
            //Order
            routes.MapRoute(
                name: "Admin-Order",
                url: "quan-tri/don-dat-hang",
                defaults: new { controller = "Orders", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Order-Cancelled",
                url: "quan-tri/don-dat-hang-da-huy",
                defaults: new { controller = "Orders", action = "CancelOrder", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //OrderDetail
            routes.MapRoute(
                name: "Admin-OrderDetail",
                url: "quan-tri/chi-tiet-don-hang",
                defaults: new { controller = "OrderDetails", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";

            //Quan ly nguoi dung
            //UserGroup
            routes.MapRoute(
                name: "Admin-UserGroup",
                url: "quan-tri/nhom-nguoi-dung",
                defaults: new { controller = "UserGroups", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-UserGroup-Create",
                url: "quan-tri/nhom-nguoi-dung/them-moi",
                defaults: new { controller = "UserGroups", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-UserGroup-Edit",
                url: "quan-tri/nhom-nguoi-dung/cap-nhat",
                defaults: new { controller = "UserGroups", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //User
            routes.MapRoute(
                name: "Admin-Account",
                url: "quan-tri/tai-khoan-nguoi-dung",
                defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Account-Create",
                url: "quan-tri/tai-khoan-nguoi-dung/them-moi",
                defaults: new { controller = "Users", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Account-Edit",
                url: "quan-tri/tai-khoan-nguoi-dung/cap-nhat",
                defaults: new { controller = "Users", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Role
            routes.MapRoute(
                name: "Admin-Role",
                url: "quan-tri/quan-ly-phan-quyen",
                defaults: new { controller = "Roles", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Role-Create",
                url: "quan-tri/quan-ly-phan-quyen/them-moi",
                defaults: new { controller = "Roles", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Role-Edit",
                url: "quan-tri/quan-ly-phan-quyen/cap-nhat",
                defaults: new { controller = "Roles", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            //Credential
            routes.MapRoute(
                name: "Admin-Credential",
                url: "quan-tri/phan-quyen-nguoi-dung",
                defaults: new { controller = "Credentials", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Credential-Create",
                url: "quan-tri/phan-quyen-nguoi-dung/them-moi",
                defaults: new { controller = "Credentials", action = "Create", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            routes.MapRoute(
                name: "Admin-Credential-Edit",
                url: "quan-tri/phan-quyen-nguoi-dung/cap-nhat",
                defaults: new { controller = "Credentials", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "ShoeShopOnline.WebApp.Areas.Admin.Controllers" }
            ).DataTokens["area"] = "Admin";
            ///Route Default
            ///
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Homes", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
