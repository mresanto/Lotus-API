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
    public class PackageBusinessImplementation : IPackagesBusiness
    {
        private readonly IPackagesRepository _repository;
        public PackageBusinessImplementation(IPackagesRepository repository)
        {
            _repository = repository;
        }
        public string FindAll()
        {
            return _repository.FindAll();
        }
        //public string FindByCategory(string category)
        //{
        //    return _repository.FindByCategory(category);
        //}
        public string FindByCode(int packcode)
        {
            return _repository.FindByCode(packcode);
        }
    }
}
