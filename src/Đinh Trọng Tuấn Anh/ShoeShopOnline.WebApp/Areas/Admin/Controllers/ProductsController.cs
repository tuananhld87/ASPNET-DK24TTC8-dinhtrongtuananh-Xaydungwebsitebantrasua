using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using ShoeShopOnline.Common;
using ShoeShopOnline.Model.DAL;
using ShoeShopOnline.WebApp.Common;

namespace ShoeShopOnline.WebApp.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        private ShoeShopOnlineDbContext db = new ShoeShopOnlineDbContext();

        // GET: Admin/Products
        public ActionResult Index(string sortOrder, int? page, string searchString, string currentFilter)
        {
            CountMessage();
            CountProduct();
            CountOrder();
            //paged
            ViewBag.CurrentSort = sortOrder;
            //Sort order
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.PromotionPriceSortParm = sortOrder == "PromotionPrice" ? "promotion_price_desc" : "PromotionPrice";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            var product = db.Product.Include(p => p.ProductCategory).Where(x => x.IsDeleted == false);

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
                product = product.Where(s => s.Name.Contains(searchString) || s.Price.ToString().Contains(searchString)
                || s.PromotionPrice.ToString().Contains(searchString) || s.ProductCategory.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                case "PromotionPrice":
                    product = product.OrderBy(s => s.PromotionPrice);
                    break;
                case "promotion_price_desc":
                    product = product.OrderByDescending(s => s.PromotionPrice);
                    break;
                case "Quantity":
                    product = product.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    product = product.OrderByDescending(s => s.Quantity);
                    break;
                default:
                    product = product.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(Guid? id)
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

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            CountMessage();
            CountOrder();
            CountProduct();
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.Id = Guid.NewGuid();
                AuditTable.InsertAuditFields(product);
                product.CreatedBy = session.UserName;
                db.Product.Add(product);
                db.SaveChanges();
                SetAlert("Thêm mới thành công", "success");
                return Redirect("/quan-tri/danh-muc-san-pham");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            CountMessage();
            CountOrder();
            CountProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                AuditTable.UpdateAuditFields(product);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.ModifiedBy = session.UserName;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Cập nhật thành công", "success");
                return Redirect("/quan-tri/danh-muc-san-pham");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Product.Find(id);
            product.IsDeleted = true;
            db.SaveChanges();
            SetAlert("Xóa thành công", "success");
            return Redirect("/quan-tri/danh-muc-san-pham");
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
