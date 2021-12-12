using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Model
{
    public class Reserve
    {
        public int rescode { get; set; }
        public DateTime resvalidity { get; set; }
        public int resamount { get; set; }
        public char statusreserve { get; set; }
        public char isdeleted { get; set; }
        public string custcpf { get; set; }
        public int packcode { get; set; }
        public DateTime paydate { get; set; }
        public float payvalue { get; set; }
        public string statuspayment {get;set;}
        public string payoption { get; set; }
    }
}
