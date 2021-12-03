using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using REST_API_Lotus.Model;
using REST_API_Lotus.Business.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using REST_API_Lotus.Business;

namespace REST_API_Lotus.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PackageController : ControllerBase
    {

        private readonly ILogger<PackageController> _logger;
        private IPackagesBusiness _packagesBusiness;

        public PackageController(ILogger<PackageController> logger, IPackagesBusiness packagesBusiness)
        {
            _logger = logger;
            _packagesBusiness = packagesBusiness;
        }
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_packagesBusiness.FindAll());
        }

        //[HttpGet("category/{category}")]
        //[ProducesResponseType((200), Type = typeof(Customer))]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(401)]
        //public IActionResult Get(string category)
        //{
        //    return Ok(_productBusiness.FindByCategory(category));
        //}
        //
        [HttpGet("{packcode}")]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int packcode)
        {
            return Ok(_packagesBusiness.FindByCode(packcode));
        }
    }
}
