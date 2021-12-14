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
    public class ReserveController : ControllerBase
    {

        private readonly ILogger<ReserveController> _logger;
        private IReserveBusiness _reserveBusiness;

        

        public ReserveController(ILogger<ReserveController> logger, IReserveBusiness reserveBusiness)
        {
            _logger = logger;
            _reserveBusiness = reserveBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(Reserve))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_reserveBusiness.FindAll());
        }

        [HttpGet("{email}")]
        [ProducesResponseType((200), Type = typeof(Reserve))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(string email)
        {
            return Ok(_reserveBusiness.FindByCode(email));
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(Reserve))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] Reserve order)
        {
            if (order == null) return BadRequest();
            return Ok(_reserveBusiness.Create(order));
        }
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(Reserve))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] Reserve order)
        {
            if (order == null) return BadRequest();
            return Ok(_reserveBusiness.Update(order));
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //     _customerService.Delete(id);
        //    return NoContent();
        //}
    }
}
