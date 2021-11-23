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
    public class ProductBusinessImplementation : IProductBusiness
    {
        private readonly IProductRepository _repository;
        public ProductBusinessImplementation(IProductRepository repository)
        {
            _repository = repository;
        }
        public string FindAll()
        {
            return _repository.FindAll();
        }
        public string FindByCode(string category)
        {
            return _repository.FindByCode(category);
        }
    }
}
