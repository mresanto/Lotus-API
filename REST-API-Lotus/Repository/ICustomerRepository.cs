using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Lotus.Repository
{
    public interface ICustomerRepository
    {
        JsonResult Create(Customer customer);
        string FindAll();
        string FindByEmail(string email);
        JsonResult Update(Customer customer);
        string Validation(string email, string cpf, string password);
        void Delete(long id);
        bool Exists(long id);
    }
}
