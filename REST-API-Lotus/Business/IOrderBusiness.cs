using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Business
{
    public interface IOrderBusiness
    {
        JsonResult Create(Order order);
        string FindAll();

    }
}
