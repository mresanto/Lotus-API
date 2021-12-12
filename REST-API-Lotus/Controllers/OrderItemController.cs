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
    public class OrderItemController : ControllerBase
    {
        private readonly ILogger<OrderItemController> _logger;
        private IOrderItemBusiness _orderItemBusiness;

        public OrderItemController(ILogger<OrderItemController> logger, IOrderItemBusiness orderItemBusiness)
        {
            _logger = logger;
            _orderItemBusiness = orderItemBusiness;
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(OrderItem))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] OrderItem orderitem)
        {
            if (orderitem == null) return BadRequest();
            return Ok(_orderItemBusiness.Create(orderitem));
        }

    }
}
