using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BJL.SurveyMaker.BL;

namespace BJL.SurveyMaker.SL.Controllers
{
    public class AnswerController : ApiController
    {
        // GET: api/Answer
        public IEnumerable<Answer> Get()
        {
            AnswerList answers = new AnswerList();
            answers.Load();
            return answers;
        }

        // GET: api/Answer/5
        public Answer Get(Guid id)
        {
            Answer answer = new Answer { Id = id };
            answer.LoadById();
            return answer;
        }

        // POST: api/Answer
        public void Post([FromBody]Answer answer)
        {
            answer.Insert();
        }

        // PUT: api/Answer/5
        public void Put(Guid id, [FromBody]Answer answer)
        {
            answer.Update();
        }

        // DELETE: api/Answer/5
        public void Delete(Guid id)
        {
            Get(id).Delete();
        }
    }
}
