using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BJL.SurveyMaker.BL;

namespace BJL.SurveyMaker.SL.Controllers
{
    public class ResponseController : ApiController
    {
        // GET: api/Response
        public IEnumerable<Response> Get()
        {
            ResponseList responses = new ResponseList();
            responses.Load();
            return responses;
        }

        // GET: api/Response/5
        public Response Get(Guid id)
        {
            Response response = new Response { Id = id };
            response.LoadById();
            return response;
        }

        // POST: api/Response
        public void Post([FromBody]Response response)
        {
            response.Insert();
        }

    }
}
