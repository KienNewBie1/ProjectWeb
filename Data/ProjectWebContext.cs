using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectWeb.Data
{
    public class ProjectWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProjectWebContext() : base("name=ProjectWebContext")
        {
        }

		public System.Data.Entity.DbSet<ProjectWeb.Models.Category> Categories { get; set; }

		public System.Data.Entity.DbSet<ProjectWeb.Models.Brand> Brands { get; set; }

		public System.Data.Entity.DbSet<ProjectWeb.Models.Product> Products { get; set; }

		public System.Data.Entity.DbSet<ProjectWeb.Models.Customer> Customers { get; set; }
	}
}
