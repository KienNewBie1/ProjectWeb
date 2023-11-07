using ProjectWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWeb.Models
{
	public class Cart
	{
		private ProjectWebContext db = new ProjectWebContext();
		public int iMaSP { get; set; }
		public string sName { get; set; }
		public string sImages { get; set; }
		public decimal dPrice { get; set; }
		public int iCount { get; set; }
		public double dTotal
		{
			get { return (double)(iCount * dPrice); }
		}

		// Khởi tạo giỏ hàng theo MaSach được truyền vào với SoLuong Mặc định là 1
		public Cart(int ms)
		{
			iMaSP = ms;
			Product s = db.Products.Single(n => n.Id == iMaSP);
			sName = s.Name;
			sImages = s.Image;
			dPrice = s.Price;
			iCount = 1;
		}
	}
}