using PagedList;
using ProjectWeb.Data;
using ProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace ProjectWeb.Controllers
{
	public class HomeController : Controller
	{
		private ProjectWebContext db = new ProjectWebContext();
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

		public ActionResult FilterProductById(int productId)
		{
			// Logic để lọc sản phẩm theo ID ở đây
			// Trả về view hoặc dữ liệu JSON tùy thuộc vào yêu cầu của bạn
			return View(db.Products.ToList());
		}

		private List<Product> LaySachMoi(int count)
		{
			return db.Products.OrderByDescending(a =>
			a.Id).Take(count).ToList();
		}
		public ActionResult Index(int? page)
		{
			var listSachMoi = LaySachMoi(20);
			int iPageNum = (page ?? 1);
			int iSize = 3;
			return View(listSachMoi.ToPagedList(iPageNum, iSize));
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
			Product product = db.Products.Find(id);
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
			return PartialView(db.Categories.ToList());
		}
		public ActionResult _PartialBrand()
		{
			return PartialView(db.Brands.ToList());
		}
		public ActionResult _PartialNavGory()
		{
			return PartialView(db.Categories.ToList());
		}
	}
}