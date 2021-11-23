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
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private IProductBusiness _productBusiness;

        public ProductController(ILogger<ProductController> logger, IProductBusiness productBusiness)
        {
            _logger = logger;
            _productBusiness = productBusiness;
        }
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_productBusiness.FindAll());
        }

        [HttpGet("{category}")]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(string category)
        {
            return Ok(_productBusiness.FindByCode(category));
        }
    }
}
