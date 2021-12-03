using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Business
{
    public interface IPackagesBusiness
    {
        string FindAll();
        //string FindByCategory(string category);
        string FindByCode(int packcode);
    }   
}
