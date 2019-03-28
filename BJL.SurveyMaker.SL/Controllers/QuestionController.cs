using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BJL.SurveyMaker.BL;

namespace BJL.SurveyMaker.SL.Controllers
{
    public class QuestionController : ApiController
    {
        // GET: api/Question
        public IEnumerable<Question> Get()
        {
            QuestionList questions = new QuestionList();
            questions.LoadQuestions();
            return questions;
        }

        // GET: api/Question/5
        public Question Get(Guid id)
        {
            Question question = new Question { Id = id };
            question.LoadQuestionById();
            return question;
        }

        // GET: api/Question?code=test1
        public Question GetQuestionByActivationCode(string code)
        {
            Question question = new Question();
            question.LoadQuestionByActivationCode(code);
            return question;
        }

        // POST: api/Question
        public void Post([FromBody]Question question)
        {
            question.InsertQuestion();
        }

        // PUT: api/Question/5
        public void Put(Guid id, [FromBody]Question question)
        {
            question.UpdateQuestion();
        }

        // DELETE: api/Question/5
        public void Delete(Guid id)
        {
            Get(id).DeleteQuestion();
        }
    }
}
