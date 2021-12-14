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
    public class OrderBusinessImplementation : IOrderBusiness
    {
        private readonly IOrderRepository _repository;
        public OrderBusinessImplementation(IOrderRepository repository)
        {
            _repository = repository;
        }
        public JsonResult Create(Order order)
        {
            return _repository.Create(order);
        }
        public string FindAll()
        {
            return _repository.FindAll();
        }

        public string FindByCode(string email)
        {
            return _repository.FindByCode(email);
        }
    }
}
