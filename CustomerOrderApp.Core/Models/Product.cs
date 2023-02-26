using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderApp.Core.Models
{
    public class Product : BaseEntity
    {
        public string pro_barcode { get; set; }
        public string pro_description { get; set; }
    }
}
