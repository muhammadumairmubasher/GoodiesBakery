using System;
using System.Collections.Generic;
using System.Text;

namespace GoodiesBakery_BO
{
    public class CartBO
    {
        public int CustomerID { get; set;}
        public int ItemID { get; set; }
        
        public int Quantity { get; set; }

        public decimal PricePerItem { get; set; }
        public decimal Amount { get; set; }

    }
}
