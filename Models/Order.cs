//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public Nullable<int> MaKH { get; set; }
        public Nullable<System.DateTime> NgayDH { get; set; }
        public Nullable<System.DateTime> NgayGiaoHang { get; set; }
        public Nullable<bool> HTGiaoHang { get; set; }
        public Nullable<bool> HTThanhToan { get; set; }
    }
}