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
    public class OrderItemBusinessImplementation : IOrderItemBusiness
    {
        private readonly IOrderItemRepository _repository;
        public OrderItemBusinessImplementation(IOrderItemRepository repository)
        {
            _repository = repository;
        }
        public JsonResult Create(OrderItem orderitem)
        {
            return _repository.Create(orderitem);
        }
      
    }
}
