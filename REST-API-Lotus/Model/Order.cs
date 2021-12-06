using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Model
{
    public class Order
    {
        public int ordcode { get; set; }
        public int prodbarcode { get; set; }
        public float itemunitaryprice { get; set; }
        public int ItemAmount { get; set; }
        public int ItemTotalPrice { get; set; }
    }
}
