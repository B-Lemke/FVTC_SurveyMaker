using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.BL;
using System.Linq;

namespace BJL.SurveyMaker.BL.Test
{
    [TestClass]
    public class utReponse
    {
        [TestMethod]
        public void LoadTest()
        {
            ResponseList responses = new ResponseList();
            responses.Load();

            int expected = 4;
            int actual = responses.Count;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void InsertTest()
        {
            Response response = new Response
            {
                QuestionId = Guid.Empty,
                AnswerId = Guid.Empty   
            };


            int rowsInserted = response.Insert();

            Assert.IsTrue(rowsInserted == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {

            //Load all responses and then get the response
            ResponseList responses = new ResponseList();
            responses.Load();
            Response response = responses.FirstOrDefault(q => q.QuestionId == Guid.Empty);

            //Change the properties
            response.QuestionId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            //Update the response
            response.Update();

            //Clear it out
            response.QuestionId = Guid.Empty;
            
            //Load it
            response.LoadById();

            Assert.AreEqual(response.QuestionId, Guid.Parse("11111111-1111-1111-1111-111111111111"));

        }

        [TestMethod]
        public void DeleteTest()
        {

            //Load all responses and then get the response
            ResponseList responses = new ResponseList();
            responses.Load();
            Response response = responses.FirstOrDefault(q => q.AnswerId == Guid.Empty);

            int actual = response.Delete();

            Assert.IsTrue(actual == 1);
        }
    }
}
