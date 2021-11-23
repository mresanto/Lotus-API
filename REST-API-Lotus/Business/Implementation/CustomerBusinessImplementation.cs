using Microsoft.Extensions.Configuration;
using REST_API_Lotus.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REST_API_Lotus.Repository;
using System.Text;

namespace REST_API_Lotus.Business.Implementation
{
    public class CustomerBusinessImplementation : ICustomerBusiness
    {
        private readonly ICustomerRepository _repository;
        public CustomerBusinessImplementation(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public JsonResult Create(Customer customer)
        {
            return _repository.Create(customer);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public string FindAll()
        {
            return _repository.FindAll();
        }
        public string FindByEmail(string email)
        {
            return _repository.FindByEmail(email);
        }
        public JsonResult Update(Customer customer)
        {
            return _repository.Update(customer);
        }

        public string Validation(string cpf, string email, string password)
        {
            if (cpf == "0")
                return _repository.Validation("0", email, password);
            else
                return _repository.Validation(cpf, email, "0");
        }
  
    }
}
