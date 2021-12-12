using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Model
{
    public class Order
    {
        public int ordCode { get; set; }
        public String ordDate { get; set; }
        public float payValue { get; set; }
        public float totalPrice { get; set; }
        public String statusOrder { get; set; }
        public String custCPF { get; set; }
        public String payDate { get; set; }
        public String isDeleted { get; set; }
        public String statusPayment { get; set; }
        public String payOption { get; set; }
    }
}
