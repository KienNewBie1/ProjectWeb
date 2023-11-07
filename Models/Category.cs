using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectWeb.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		//[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập tên của danh mục: ")]
		public string Description { get; set; }
		//public string? Category_desc { get; set; }
	}
}