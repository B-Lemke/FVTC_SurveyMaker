using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.BL;
using System.Linq;

namespace BJL.SurveyMaker.BL.Test
{
    [TestClass]
    public class utQuestion
    {
       
        [TestMethod]
        public void LoadTest()
        {
            QuestionList questions = new QuestionList();
            questions.Load();

            int expected = 6;
            int actual = questions.Count;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void InsertTest()
        {
            Question question = new Question();

            question.Text = "TestQuestion";

            int rowsInserted = question.Insert();

            Assert.IsTrue(rowsInserted > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {

            //Load all questions and then get the question
            QuestionList questions = new QuestionList();
            questions.Load();
            Question question = questions.FirstOrDefault(q => q.Text == "TestQuestion");

            //Change the properties
            question.Text = "ChangedText";

            //Update the question
            question.Update();

            //Load it
            question.LoadById();

            Assert.AreEqual(question.Text, "ChangedText");

        }

        [TestMethod]
        public void DeleteTest()
        {

            //Load all questions and then get the question
            QuestionList questions = new QuestionList();
            questions.Load();
            Question question = questions.FirstOrDefault(q => q.Text == "ChangedText");

            int actual = question.Delete();

            Assert.IsTrue(actual > 0);
        }


    }
    
}
