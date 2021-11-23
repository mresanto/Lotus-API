using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Model
{
    public class Product
    {
        public int ProdBarCode { get; set; }
        public string ProdName { get; set; }
        public float ProdPrice { get; set; }
        public string IsDeleted { get; set; }
        public string CatCode { get; set; }
        public string CatName { get; set; }

       // public int ImgCode { get; set; }
       // public string ImgType { get; set; }
       // public string Image { get; set; }
       // public string ImgTitle { get; set; }

    }
}
