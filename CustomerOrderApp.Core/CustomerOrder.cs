using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core
{
    public class CustomerOrder : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }

        //public string DeliveryAddress { get; set; }
    }
}
