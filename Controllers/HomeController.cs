using ProjectWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Controllers
{
	public class HomeController : Controller
	{
		private ProjectWebContext db = new ProjectWebContext();
		public ActionResult Index()
		{
			return View(db.Products.ToList());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
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