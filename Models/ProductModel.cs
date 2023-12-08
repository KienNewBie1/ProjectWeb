using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWeb.Models
{
    
    public class ProductModel
	{
        private ShopWatchEntities db = new ShopWatchEntities();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public Nullable<int> Brand_Id { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        public virtual Brands Brands { get; set; }
        public virtual Categories Categories { get; set; }
        public List<Products> SearchByKey(string key)
        {
            return db.Products.SqlQuery("Select * from Products Where Name like '%" + key + "%'").ToList();
        }
    }
}