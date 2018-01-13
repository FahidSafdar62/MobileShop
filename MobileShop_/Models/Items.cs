using System;
using System.Collections.Generic;

namespace MobileShop_.Models
{
    public partial class Items
    {
        public Items()
        {
            Purchase = new HashSet<Purchase>();
            Sales = new HashSet<Sales>();
        }

        public int ItemsId { get; set; }
        public int CatagoryId { get; set; }
        public string ItemsName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public Catagory Catagory { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
        public ICollection<Sales> Sales { get; set; }
    }
}
