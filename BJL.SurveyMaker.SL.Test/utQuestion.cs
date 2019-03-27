using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BJL.SurveyMaker.BL;
using System.Linq;

namespace BJL.SurveyMaker.SL.Test
{
    [TestClass]
    public class utQuestion
    {
        [TestMethod]
        public void GetAll()
        {
            HttpClient client = InitializeClient();
            string result;
            dynamic items;
            HttpResponseMessage response;

            //Call the API
            response = client.GetAsync("Question").Result;

            //Proces response
            result = response.Content.ReadAsStringAsync().Result;

            //Put json into the color list
            items = (JArray)JsonConvert.DeserializeObject(result);
            QuestionList questions = new QuestionList();
            questions = items.ToObject<QuestionList>();

            Assert.AreEqual(questions.Count, 6);
        }


        [TestMethod]
        public void Insert()
        {
            Question question = new Question();
            question.Text = "TestQuestion";

            HttpClient client = InitializeClient();
            //Serialize a color object that we're trying to insert
            string serializedQuestion = JsonConvert.SerializeObject(question);
            var content = new StringContent(serializedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync("Question", content).Result;

            QuestionList questions = new QuestionList();
            questions.LoadQuestions();
            question = questions.FirstOrDefault(q => q.Text == "TestQuestion");


            HttpResponseMessage responseRetrieved = client.GetAsync("Question/" + question.Id).Result;
            string result = responseRetrieved.Content.ReadAsStringAsync().Result;

            Question retrievedQuestion = JsonConvert.DeserializeObject<Question>(result);

            Assert.IsTrue(retrievedQuestion.Text == "TestQuestion");
        }



        [TestMethod]
        public void Update()
        {
            Question question = new Question();
            QuestionList questions = new QuestionList();
            questions.LoadQuestions();
            question = questions.FirstOrDefault(q => q.Text == "TestQuestion");

            question.Text = "UpdatedQuestion";


            HttpClient client = InitializeClient();
            //Serialize a color object that we're trying to insert
            string serializedQuestion = JsonConvert.SerializeObject(question);
            var content = new StringContent(serializedQuestion);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("Question/" + question.Id, content).Result;

            Question newQuestion = new Question { Id = question.Id };
            HttpResponseMessage responseRetrieved = client.GetAsync("Question/" + question.Id).Result;
            string result = responseRetrieved.Content.ReadAsStringAsync().Result;
            newQuestion = JsonConvert.DeserializeObject<Question>(result);


            Assert.IsTrue(newQuestion.Text == "UpdatedQuestion");
        }

        [TestMethod]
        public void Delete()
        {
            Question question = new Question();
            QuestionList questions = new QuestionList();
            questions.LoadQuestions();
            question = questions.FirstOrDefault(q => q.Text == "UpdatedQuestion");
   
            HttpClient client = InitializeClient();
            //Serialize a color object that we're trying to insert
            HttpResponseMessage response = client.DeleteAsync("Question/" + question.Id).Result;

            Question newQuestion = new Question { Id = question.Id };
            HttpResponseMessage responseRetrieved = client.GetAsync("Question/" + question.Id).Result;
            string result = responseRetrieved.Content.ReadAsStringAsync().Result;
            newQuestion = JsonConvert.DeserializeObject<Question>(result);


            Assert.IsNull(newQuestion.Text);
        }


        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://bjlsurveymaker.azurewebsites.net/api/");
            return client;
        }
    }
}
