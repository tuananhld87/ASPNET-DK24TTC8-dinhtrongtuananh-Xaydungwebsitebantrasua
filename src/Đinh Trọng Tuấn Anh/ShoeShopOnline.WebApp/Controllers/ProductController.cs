using PagedList;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShoeShopOnline.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();
        // GET: Products
        public ActionResult LoadHotProduct()
        {
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false && x.TopHot == true).OrderByDescending(x => x.CreatedOn).Take(2).ToList();
            ViewBag.Count = list.Count();
            return PartialView(list);
        }
        public ActionResult LoadNormalProduct1()
        {
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false && x.IsNormalProduct1 == true).OrderByDescending(x => x.CreatedOn).Take(4).ToList();
            ViewBag.Count = list.Count();
            return PartialView(list);
        }
        public ActionResult LoadNormalProduct2()
        {
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false && x.IsNormalProduct2 == true).OrderByDescending(x => x.CreatedOn).Take(4).ToList();
            ViewBag.Count = list.Count();
            return PartialView(list);
        }
        public ActionResult LoadNewProduct()
        {
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false && x.IsNewProduct == true).OrderByDescending(x => x.CreatedOn).Take(3).ToList();
            ViewBag.Count = list.Count();
            return PartialView(list);
        }

        public ActionResult LoadLastedProduct(Guid? id)
        {
            Product product = db.Product.Find(id);
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false && x.ProductCategoryId == product.ProductCategoryId && x.Id != product.Id).OrderByDescending(x => x.CreatedOn).Take(3).ToList();
            ViewBag.Count = list.Count();
            return PartialView(list);
        }

        //Detail of Product
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        //Load category for menu
        public ActionResult LoadCategory()
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            productCategories = db.ProductCategory.Where(x => x.IsDeleted == false).ToList();
            var product = db.Product.ToList();
            ViewBag.Count = productCategories.Count();
            ViewBag.CountAllProduct = product.Count();
            return PartialView(productCategories);
        }
        //Load product from price
        public ActionResult CategoryDetail(ProductCategory productCategory)
        {
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false && x.ProductCategoryId == productCategory.Id).ToList();
            ViewBag.Count = list.Count();
            return View(list);
        }
        //Load all product in 1 page
        public ActionResult LoadAllProduct(int? page)
        {
            List<Product> list = new List<Product>();
            list = db.Product.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList();
            ViewBag.Count = list.Count();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        //Search auto complete
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword)
        {
            var model = new ProductDao().Search(keyword);
            ViewBag.Keyword = keyword;
            ViewBag.Count = model.Count();
            return View(model);
        }
    }
}