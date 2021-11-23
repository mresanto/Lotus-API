using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Business
{
    public interface ICustomerBusiness
    {
        JsonResult Create(Customer customer);
        string FindAll();
        string FindByEmail(string email);
        JsonResult Update(Customer customer);
        string Validation(string cpf, string email, string password);
        void Delete(long id);

    }
}
