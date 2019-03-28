using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJL.SurveyMaker.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace BJL.SurveyMaker.QuizzerUI
{
    public partial class Quizzer : System.Web.UI.Page
    {
        Question question;
        Answer answer;
        AnswerList answers;
        Activation activation;
        ActivationList activations;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmitCode_Click(object sender, EventArgs e)
        {
            try
            {
                activations = new ActivationList();
                activation = new Activation();
                activations.Load();

                var match = activations.FirstOrDefault(a => a.ActivationCode == txtCode.Text);

                if (match != null)
                {
                    lblQuestion.Visible = true;
                }
                else
                {
                    throw new Exception("Please insert a correct code.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
        }

        protected void btnSubmitAnswer_Click(object sender, EventArgs e)
        {

        }

        /*
        private void LoadSL()
        {
            HttpClient client = InitializeClient();

            string result;
            dynamic items;
            HttpResponseMessage response;

            response = client.GetAsync("Activation").Result;

            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Process the response
                    result = response.Content.ReadAsStringAsync().Result;

                    items = (JArray)JsonConvert.DeserializeObject(result);
                    activations = items.ToObject<ActivationList>();                    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51924/api/");
            return client;
        }
        */
    }
}