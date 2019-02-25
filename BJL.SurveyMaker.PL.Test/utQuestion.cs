using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.PL;
using System.Linq;

namespace BJL.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestion
    {
        [TestMethod]
        public void LoadTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                int expected = 6;

                var questions = dc.tblQuestions;

                int actual = questions.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblQuestion question = new tblQuestion();
                question.Id = Guid.NewGuid();
                question.Text = "TestQuestion";

                dc.tblQuestions.Add(question);

                dc.SaveChanges();

                tblQuestion retrievedQuestion = dc.tblQuestions.FirstOrDefault(q => q.Text == "TestQuestion");

                Assert.AreEqual(question.Id, retrievedQuestion.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblQuestion question = dc.tblQuestions.FirstOrDefault(q => q.Text == "TestQuestion");

                question.Text = "UpdatedTestQuestion";

                dc.SaveChanges();

                tblQuestion retrievedQuestion = dc.tblQuestions.FirstOrDefault(q => q.Text == "UpdatedTestQuestion");

                Assert.IsNotNull(retrievedQuestion);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblQuestion question = dc.tblQuestions.FirstOrDefault(q => q.Text == "UpdatedTestQuestion");

                dc.tblQuestions.Remove(question);

                dc.SaveChanges();

                tblQuestion retrievedQuestion = dc.tblQuestions.FirstOrDefault(q => q.Text == "UpdatedTestQuestion");

                Assert.IsNull(retrievedQuestion);
            }
        }
    }
}
