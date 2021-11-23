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
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private ICustomerBusiness _customerBusiness;

        

        public CustomerController(ILogger<CustomerController> logger, ICustomerBusiness customerBusiness)
        {
            _logger = logger;
            _customerBusiness = customerBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_customerBusiness.FindAll());
        }

        [HttpGet("{email}")]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(string email)
        {
            return Ok(_customerBusiness.FindByEmail(email));
        }
        [HttpGet("{cpf}/{email}/{password}")]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get(string cpf, string email, string password)
        {
            var customer = _customerBusiness.Validation(cpf, email, password);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();
            return Ok(_customerBusiness.Create(customer));
        }
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(Customer))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] Customer customer)
        {
            if (customer == null) return BadRequest();
            return Ok(_customerBusiness.Update(customer));
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //     _customerService.Delete(id);
        //    return NoContent();
        //}
    }
}
