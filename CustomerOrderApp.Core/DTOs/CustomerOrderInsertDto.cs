using CustomerOrderApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core.DTOs
{
    public class CustomerOrderInsertDto
    {
        public int cust_ord_cust_id { get; set; }
        public string cust_ord_no { get; set; }
        public string cust_ord_name { get; set; }
        public string cust_ord_address { get; set; }

        public List<CustomProductDto> CustomProductDto { get; set; }
    }
}
