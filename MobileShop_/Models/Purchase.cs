using System;
using System.Collections.Generic;

namespace MobileShop_.Models
{
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public int VenderId { get; set; }
        public int ItemsId { get; set; }
        public int Pquanity { get; set; }
        public int PricePerUnit { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }

        public Items Items { get; set; }
        public Vender Vender { get; set; }
    }
}
