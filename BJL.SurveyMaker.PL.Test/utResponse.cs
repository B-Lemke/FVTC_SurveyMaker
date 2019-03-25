using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.PL;
using System.Linq;

namespace BJL.SurveyMaker.PL.Test
{
    [TestClass]
    public class utResponse
    {
        [TestMethod]
        public void LoadTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                int expected = 4;

                var responses = dc.tblResponses;

                int actual = responses.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                //get a question and answer
                tblAnswer answer = dc.tblAnswers.FirstOrDefault(a => a.Text == "Kelly Kapoor");
                tblQuestion question = dc.tblQuestions.FirstOrDefault(r => r.Text == "Who sprouts mung beans in their desk drawers?");

                //set properties
                tblResponse response = new tblResponse();
                response.Id = Guid.NewGuid();
                response.QuestionId = question.Id;
                response.AnswerId = answer.Id;

                dc.tblResponses.Add(response);

                int results = dc.SaveChanges();

                Assert.IsTrue(results > 0);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                //get a question and answer
                tblAnswer answer = dc.tblAnswers.FirstOrDefault(a => a.Text == "Kelly Kapoor");
                tblQuestion question = dc.tblQuestions.FirstOrDefault(r => r.Text == "Who sprouts mung beans in their desk drawers?");

                tblResponse response = dc.tblResponses.FirstOrDefault(r => (r.AnswerId == answer.Id) && (r.QuestionId == question.Id));
                tblAnswer otherAnswer = dc.tblAnswers.FirstOrDefault(a => a.Text == "Michael Scott");
                response.AnswerId = otherAnswer.Id;

                int results = dc.SaveChanges();

                Assert.IsTrue(results > 0);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                //get a question and answer
                tblAnswer answer = dc.tblAnswers.FirstOrDefault(a => a.Text == "Michael Scott");
                tblQuestion question = dc.tblQuestions.FirstOrDefault(r => r.Text == "Who sprouts mung beans in their desk drawers?");

                tblResponse response = dc.tblResponses.FirstOrDefault(r => (r.AnswerId == answer.Id) && (r.QuestionId == question.Id));

                dc.tblResponses.Remove(response);

                int results = dc.SaveChanges();

                Assert.IsTrue(results > 0);
            }
        }
    }
}
