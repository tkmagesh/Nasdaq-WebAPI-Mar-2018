using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyApiProject.Models;

namespace MyApiProject.Controllers
{
    public class CalculatorController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Calculate([FromUri] Operation op, [FromBody] Numbers nos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = 0;
            switch (op.OpName)
            {
                case "Add":
                    result = nos.N1 + nos.N2;
                    break;
                case "Subtract":
                    result = nos.N1 - nos.N2;
                    break;
                default:
                    break;
            }
            return Ok(result);
        }        
    }
}
