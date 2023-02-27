using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core.Models
{
    public class CustomerOrder : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string cust_order_no { get; set; }
        public string cust_ord_address { get; set; }
        public string cust_ord_barcode { get; set; }
        public string cust_ord_description { get; set; }
        public int cust_ord_quantity { get; set; }
        public decimal cust_ord_price { get; set; }
    }
}
