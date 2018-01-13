using System;
using System.Collections.Generic;

namespace MobileShop_.Models
{
    public partial class Vender
    {
        public Vender()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int Vid { get; set; }
        public string Vname { get; set; }
        public int Vmobile { get; set; }

        public ICollection<Purchase> Purchase { get; set; }
    }
}
