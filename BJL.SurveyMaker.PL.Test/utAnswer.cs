using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.PL;
using System.Linq;

namespace BJL.SurveyMaker.PL.Test
{
    [TestClass]
    public class utAnswer
    {
        [TestMethod]
        public void LoadTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                int expected = 8;

                var answers = dc.tblAnswers;

                int actual = answers.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblAnswer answer = new tblAnswer();
                answer.Id = Guid.NewGuid();
                answer.Text = "TestAnswer";

                dc.tblAnswers.Add(answer);

                dc.SaveChanges();

                tblAnswer retrievedAnswer = dc.tblAnswers.FirstOrDefault(q => q.Text == "TestAnswer");

                Assert.AreEqual(answer.Id, retrievedAnswer.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblAnswer answer = dc.tblAnswers.FirstOrDefault(q => q.Text == "TestAnswer");

                answer.Text = "UpdatedTestAnswer";

                dc.SaveChanges();

                tblAnswer retrievedAnswer = dc.tblAnswers.FirstOrDefault(q => q.Text == "UpdatedTestAnswer");

                Assert.IsNotNull(retrievedAnswer);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblAnswer answer = dc.tblAnswers.FirstOrDefault(q => q.Text == "UpdatedTestAnswer");

                dc.tblAnswers.Remove(answer);

                dc.SaveChanges();

                tblAnswer retrievedAnswer = dc.tblAnswers.FirstOrDefault(q => q.Text == "UpdatedTestAnswer");

                Assert.IsNull(retrievedAnswer);
            }
        }
    }
}
