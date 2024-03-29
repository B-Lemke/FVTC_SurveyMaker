﻿using System;
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
            questions.LoadQuestions();

            int expected = 6;
            int actual = questions.Count;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void InsertTest()
        {
            Question question = new Question();

            question.Text = "TestQuestion";

            int rowsInserted = question.InsertQuestion();

            Assert.IsTrue(rowsInserted > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {

            //Load all questions and then get the question
            QuestionList questions = new QuestionList();
            questions.LoadQuestions();
            Question question = questions.FirstOrDefault(q => q.Text == "TestQuestion");

            //Change the properties
            question.Text = "ChangedText";

            //Update the question
            question.UpdateQuestion();

            //Load it
            question.LoadQuestionById();

            Assert.AreEqual(question.Text, "ChangedText");

        }

        [TestMethod]
        public void DeleteTest()
        {

            //Load all questions and then get the question
            QuestionList questions = new QuestionList();
            questions.LoadQuestions();
            Question question = questions.FirstOrDefault(q => q.Text == "ChangedText");

            int actual = question.DeleteQuestion();

            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void LoadAnswers()
        {
            //Load all questions and then get the question
            QuestionList questions = new QuestionList();
            
            //Load answers is executed in LoadQuestions, check that they are populated
            questions.LoadQuestions();

            Question question = questions.FirstOrDefault(q => q.Text == "Who sprouts mung beans in their desk drawers?");

            int expected = 4;
            int actual = question.Answers.Count;

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void LoadByActivationCodePass()
        {
            Question question = new Question();

            question.LoadQuestionByActivationCode("test1");

            Assert.AreEqual(question.Text, "Which character is theorized by many fans to be the Scranton Strangler?");
        }


        [TestMethod]
        public void LoadByActivationCodeFailLate()
        {
            Question question = new Question();

            //Activation code already passed
            question.LoadQuestionByActivationCode("test2");

            Assert.AreEqual(question.Text, "Question no longer active");

        }

        [TestMethod]
        public void LoadByActivationCodeFailEarly()
        {
            Question question = new Question();

            //Activation code already passed
            question.LoadQuestionByActivationCode("test3");

            Assert.AreEqual(question.Text, "Question not active yet");

        }


        [TestMethod]
        public void LoadByActivationCodeFailInvalid()
        {
            Question question = new Question();

            //Activation code already passed
            question.LoadQuestionByActivationCode("zzzzz");

            Assert.AreEqual(question.Text, "Non-Valid Activation Code");

        }
    }
}
