using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BJL.SurveyMaker.BL;

namespace BJL.SurveyMaker.SL.Controllers
{
    public class ActivationController : ApiController
    {
        // GET: api/Activation
        public IEnumerable<Activation> Get()
        {
            ActivationList activations = new ActivationList();
            activations.Load();
            return activations;
        }

        // GET: api/Activation/5
        public Activation Get(Guid id)
        {
            Activation activation = new Activation { Id = id };
            activation.LoadById();
            return activation;
        }

        // GET: api/Activation/5
        public Activation GetByQuestionId(Guid questionId)
        {
            Activation activation = new Activation();
            activation.LoadByQuestionId(questionId);
            return activation;
        }

        // POST: api/Activation
        public void Post([FromBody]Activation activation)
        {
            activation.Insert();
        }

        // PUT: api/Activation/5
        public void Put(Guid id, [FromBody]Activation activation)
        {
            activation.Update();
        }

        // DELETE: api/Activation/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
