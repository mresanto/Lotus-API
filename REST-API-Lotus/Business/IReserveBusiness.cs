using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Business
{
    public interface IReserveBusiness
    {
        JsonResult Create(Reserve reserve);
        string FindAll();
        string FindByCode(string email);
        JsonResult Update(Reserve reserve);

    }
}
