using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Repository
{
    public interface IPackagesRepository
    {
        string FindAll();
        //string FindByCategory(string category);
        string FindByCode(int packcode);
    }   
}
