using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core.DTOs
{
    public class CustomerOrderQuantityUpdateDto
    {
        public int cust_ord_cust_id { get; set; }
        public string cust_ord_order_no { get; set; }
        public string cust_ord_barcode { get; set; }
        public int cust_ord_quantity { get; set; }
    }
}
