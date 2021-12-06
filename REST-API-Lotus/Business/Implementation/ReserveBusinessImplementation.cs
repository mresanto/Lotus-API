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
    public class ReserveBusinessImplementation : IReserveBusiness
    {
        private readonly IReserveRepository _repository;
        public ReserveBusinessImplementation(IReserveBusiness repository)
        {
            _repository = repository;
        }
        public JsonResult Create(Reserve reserve)
        {
            return _repository.Create(reserve);
        }
        public string FindAll()
        {
            return _repository.FindAll();
        }
        public JsonResult Update(Reserve reserve)
        {
            return _repository.Update(reserve);
        }
    }
}
