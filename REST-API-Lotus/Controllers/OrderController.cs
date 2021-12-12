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
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private IOrderBusiness _orderBusiness;        

        public OrderController(ILogger<OrderController> logger, IOrderBusiness orderBusiness)
        {
            _logger = logger;
            _orderBusiness = orderBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(Order))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_orderBusiness.FindAll());
        }
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(Order))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null) return BadRequest();
            return Ok(_orderBusiness.Create(order));
        }
    }
}
