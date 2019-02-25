using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.PL;
using System.Linq;

namespace BJL.SurveyMaker.PL.Test
{
    [TestClass]
    public class utQuestionAnswerAnswer
    {
        [TestMethod]
        public void LoadTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                int expected = 12;

                var questionAnswers = dc.tblQuestionAnswers;

                int actual = questionAnswers.Count();

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
                tblQuestion question = dc.tblQuestions.FirstOrDefault(q => q.Text == "Who sprouts mung beans in their desk drawers?");

                //set properties
                tblQuestionAnswer questionAnswer = new tblQuestionAnswer();
                questionAnswer.Id = Guid.NewGuid();
                questionAnswer.QuestionId = question.Id;
                questionAnswer.AnswerId = answer.Id;
                questionAnswer.IsCorrect = false;

                dc.tblQuestionAnswers.Add(questionAnswer);

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
                tblQuestion question = dc.tblQuestions.FirstOrDefault(q => q.Text == "Who sprouts mung beans in their desk drawers?");

                tblQuestionAnswer questionAnswer = dc.tblQuestionAnswers.FirstOrDefault(q => (q.AnswerId == answer.Id) && (q.QuestionId == question.Id));

                questionAnswer.IsCorrect = true;

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
                tblAnswer answer = dc.tblAnswers.FirstOrDefault(a => a.Text == "Kelly Kapoor");
                tblQuestion question = dc.tblQuestions.FirstOrDefault(q => q.Text == "Who sprouts mung beans in their desk drawers?");

                tblQuestionAnswer questionAnswer = dc.tblQuestionAnswers.FirstOrDefault(q => (q.AnswerId == answer.Id) && (q.QuestionId == question.Id));

                dc.tblQuestionAnswers.Remove(questionAnswer);

                int results = dc.SaveChanges();

                Assert.IsTrue(results > 0);
            }
        }
    }
}
