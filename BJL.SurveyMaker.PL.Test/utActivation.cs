using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BJL.SurveyMaker.PL;
using System.Linq;

namespace BJL.SurveyMaker.PL.Test
{
    [TestClass]
    public class utActivation
    {
        [TestMethod]
        public void LoadTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                int expected = 3;

                var activations = dc.tblActivations;

                int actual = activations.Count();

                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod]
        public void InsertTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblActivation activation = new tblActivation();
                activation.Id = Guid.NewGuid();
                activation.StartDate = DateTime.Now;
                activation.EndDate = DateTime.Now.AddYears(10);
                activation.QuestionId = dc.tblQuestions.FirstOrDefault(q => q.Text == "Who sprouts mung beans in their desk drawers?").Id;
                activation.ActivationCode = "utest";

                dc.tblActivations.Add(activation);

                dc.SaveChanges();

                tblActivation retrievedActivation = dc.tblActivations.FirstOrDefault(a => a.ActivationCode == "utest");

                Assert.AreEqual(activation.Id, retrievedActivation.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblActivation activation = dc.tblActivations.FirstOrDefault(a => a.ActivationCode == "utest");

                activation.ActivationCode = "updat";

                dc.SaveChanges();

                tblActivation retrievedActivation = dc.tblActivations.FirstOrDefault(a => a.ActivationCode == "updat");

                Assert.IsNotNull(retrievedActivation);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (SurveyEntities dc = new SurveyEntities())
            {
                tblActivation activation = dc.tblActivations.FirstOrDefault(a => a.ActivationCode == "updat");

                dc.tblActivations.Remove(activation);

                dc.SaveChanges();

                tblActivation retrievedActivation = dc.tblActivations.FirstOrDefault(a => a.ActivationCode == "updat");

                Assert.IsNull(retrievedActivation);
            }
        }
    }
}
