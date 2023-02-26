using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core.Models
{
    public class Customer : BaseEntity
    {
        public string cust_name { get; set; }
        public string cust_address { get; set; }
    }
}
