using System;
using System.Collections.Generic;

namespace MobileShop_.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sales>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }

        public ICollection<Sales> Sales { get; set; }
    }
}
