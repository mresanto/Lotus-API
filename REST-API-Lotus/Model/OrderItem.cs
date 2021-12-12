using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Model
{
    public class OrderItem
    {
        public int prodBarCode { get; set; }
        public float itemUnitaryPrice { get; set; }
        public int itemAmount { get; set; }
        public float totalPrice { get; set; }
        public String custCPF { get; set;}

    }
}
