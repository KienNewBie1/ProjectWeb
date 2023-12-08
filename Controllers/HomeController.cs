using MailKit.Search;
using PagedList;
using ProjectWeb.Data;
using ProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
namespace ProjectWeb.Controllers
{
	public class HomeController : Controller
	{
		private ShopWatchEntities db = new ShopWatchEntities();
		//private List<Product> LaySachMoi(int count)
		//{
		//	return db.Products.OrderByDescending(a =>
		//	a.Id).Take(count).ToList();
		//}
		public ActionResult PartialCategoryID(int id)
		{
			//ViewBag.MaCD = id;
			//int iSize = 3;
			//int iPageNum = (page ?? 1);

			var sach = from s in db.Categories where s.Id == id select s;
			return View(sach);
		}
		public ActionResult aa(string sortProperty, string sortOrder)
		{

			var sach = from s in db.Products select s;
			ViewBag.SortOrder = String.IsNullOrEmpty(sortOrder) ? "desc" : "";
			if (String.IsNullOrEmpty(sortProperty)) sortProperty = "Price";
			if (sortOrder == "desc")
				sach = sach.OrderBy(sortProperty + " desc");
			else
				sach = sach.OrderBy(sortProperty);
			return View(sach);
		}
		public ActionResult FilterProductById(int productId)
		{
			// Logic để lọc sản phẩm theo ID ở đây
			// Trả về view hoặc dữ liệu JSON tùy thuộc vào yêu cầu của bạn
			return View(db.Products.ToList());
		}

		private List<Products> LaySachMoi(int count)
		{
			return db.Products.OrderByDescending(a =>
			a.Id).Take(count).ToList();
		}
		private List<Products> GetProductsByName(int count)
		{
			return db.Products.OrderByDescending(a =>
			a.Name).Take(count).ToList();
		}
		private List<Products> GetProductsById(int count)
		{
			return db.Products.OrderByDescending(a =>
			a.CategoryId).Take(count).ToList();
		}

		public ActionResult Index( int? page, string searchString/* int categoryID = 0*/)
		{
			var listSachMoi = LaySachMoi(20);
			var listProName = GetProductsByName(20);
			var listProId = GetProductsById(20);
			int iPageNum = (page ?? 1);
			int iSize = 3;
			var link = (from l in db.Products select l).OrderBy("Id");
			// 1. Đoạn code dùng để tìm kiếm
			// 1.1. Lưu tư khóa tìm kiếm
			ViewBag.Keyword = searchString;
			//1.2 Lưu chủ đề tìm kiếm
			//ViewBag.Subject = categoryID;
			//var sp = from s in db.Products select s;
			//sp = sp.Where(n => n.Name == searchString).OrderBy("Name");

			var sp = (from s in db.Products select s ).OrderBy("Id");
			//1.3. Tìm kiếm theo searchString
			if (!String.IsNullOrEmpty(searchString))
			{
				link = link.Where(s => s.Name.Contains(searchString));
			}	
	
				
			//1.4. Tìm kiếm theo CategoryID
			
			//sp = sp.Where(b => b.CategoryName == searchString).OrderBy("Id");

			//1.5. Tìm kiếm theo CategoryID
			//if (categoryID != 0)
			//	listProId = GetProductsById(20);
				//sp = sp.Where(c => c.CategoryId == categoryID).OrderBy("id");

				//1.6. Tìm kiếm theo Danh sách chủ đề
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name"); // danh sách Category               
			return View(link.ToPagedList(iPageNum, iSize));
		}

		//public ActionResult Index1(int? page)
		//{
		//	var listSachMoi = LaySachMoi(20);
		//	int iPageNum = (page ?? 1);
		//	int iSize = 3;
		//	return View(listSachMoi.ToPagedList(iPageNum, iSize));
		//	//return View(db.Products.ToList());
		//}
		public ActionResult ProductById(int id,int? page)
		{
			ViewBag.Id = id;
			var sp = from s in db.Products select s;
			sp = sp.Where(n => n.CategoryId == id).OrderBy("Id");
			int iPageNum = (page ?? 1);
			int iSize = 3;
			
			
			return View(sp.ToPagedList(iPageNum, iSize));
			//return View(db.Products.ToList());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}
		public ActionResult DetailProduct(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Products product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
			//return View(db.Products.ToList());
		}
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		public ActionResult _PartialNavbar()
		{
			return PartialView(db.Categories.ToList());
		}
		public ActionResult _PartialCategory()
		{
			var listChuDe = from cd in db.Categories select cd;
			return PartialView(listChuDe);
		}
		public ActionResult _PartialBrand()
		{
			return PartialView(db.Brands.ToList());
		}
		public ActionResult _PartialNavGory()
		{
			return PartialView(db.Categories.ToList());
		}
		public ActionResult Search()
		{
			
			return PartialView(db.Products.ToList());
		}
	}
}