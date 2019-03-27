using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.BL;
using System.Linq;

namespace BJL.SurveyMaker.BL.Test
{
    [TestClass]
    public class utActivation
    {
        [TestMethod]
        public void LoadTest()
        {
            ActivationList activations = new ActivationList();
            activations.Load();

            int expected = 3;
            int actual = activations.Count;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void InsertTest()
        {
            Activation activation = new Activation
            {
                QuestionId = Guid.Empty,
                ActivationCode = "utest",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
            };


            int rowsInserted = activation.Insert();

            Assert.IsTrue(rowsInserted == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {

            //Load all activations and then get the activation
            ActivationList activations = new ActivationList();
            activations.Load();
            Activation activation = activations.FirstOrDefault(a => a.QuestionId == Guid.Empty);

            //Change the properties
            activation.ActivationCode = "updte";

            //Update the activation
            int rowsAffected = activation.Update();


            Assert.IsTrue(rowsAffected == 1);

        }

        [TestMethod]
        public void DeleteTest()
        {

            //Load all activations and then get the activation
            ActivationList activations = new ActivationList();
            activations.Load();
            Activation activation = activations.FirstOrDefault(a => a.ActivationCode == "updte");

            int rowsAffected = activation.Delete();

            Assert.IsTrue(rowsAffected == 1);
        }
    }
}
