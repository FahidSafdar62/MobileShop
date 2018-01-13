using System;
using System.Collections.Generic;

namespace MobileShop_.Models
{
    public partial class Sales
    {
        public int SalesId { get; set; }
        public int CustomerId { get; set; }
        public int ItemsId { get; set; }
        public int Quantity { get; set; }
        public int PricePerUnit { get; set; }
        public int TotalPrice { get; set; }
        public DateTime TrDate { get; set; }

        public Customer Customer { get; set; }
        public Items Items { get; set; }
    }
}
