using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core.DTOs
{
    public class CustomProductDto
    {
        public string cust_ord_barcode { get; set; }
        public string cust_ord_description { get; set; }
        public int cust_ord_quantity { get; set; }
        public decimal cust_ord_price { get; set; }
    }
}
