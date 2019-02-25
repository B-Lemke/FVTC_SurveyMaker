using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.BL;
using System.Linq;

namespace BJL.SurveyMaker.BL.Test
{
    [TestClass]
    public class utAnswer
    {
        [TestMethod]
        public void LoadTest()
        {
            AnswerList answers = new AnswerList();
            answers.Load();

            int expected = 8;
            int actual = answers.Count;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void InsertTest()
        {
            Answer answer = new Answer();

            answer.Text = "TestAnswer";

            int rowsInserted = answer.Insert();

            Assert.IsTrue(rowsInserted > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {

            //Load all answers and then get the answer
            AnswerList answers = new AnswerList();
            answers.Load();
            Answer answer = answers.FirstOrDefault(q => q.Text == "TestAnswer");

            //Change the properties
            answer.Text = "ChangedText";

            //Update the answer
            answer.Update();

            //Load it
            answer.LoadById();

            Assert.AreEqual(answer.Text, "ChangedText");

        }

        [TestMethod]
        public void DeleteTest()
        {

            //Load all answers and then get the answer
            AnswerList answers = new AnswerList();
            answers.Load();
            Answer answer = answers.FirstOrDefault(q => q.Text == "ChangedText");

            int actual = answer.Delete();

            Assert.IsTrue(actual > 0);
        }
    }
}
