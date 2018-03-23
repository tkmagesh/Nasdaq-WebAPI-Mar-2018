using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApiProject.Controllers
{
    public class ValuesController : ApiController
    {
        private static List<string> data = new List<string> { "value1", "value2" };

        // GET api/values
        public IEnumerable<string> Get()
        {
            return data;
        }

        // GET api/values/5
        public string Get(int id)
        {
            if (id < data.Count)
                return data[id];
            var errorMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            errorMessage.Content = new StringContent("Invalid id");
            errorMessage.ReasonPhrase = "Not Found";
            throw new HttpResponseException(errorMessage);
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            data.Add(value);
            return value;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
