using System;
using System.Collections.Generic;

namespace MobileShop_.Models
{
    public partial class Catagory
    {
        public Catagory()
        {
            Items = new HashSet<Items>();
        }

        public int CatagoryId { get; set; }
        public string CatagoryName { get; set; }
		public DateTime Date { get; set; }

		public ICollection<Items> Items { get; set; }
    }
}
