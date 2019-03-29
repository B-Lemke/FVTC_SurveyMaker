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
        QuestionList questions;
        Answer answer;
        AnswerList answers;
        Activation activation;
        ActivationList activations;
        Response response;
        ResponseList responses;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                activations = new ActivationList();
                answers = new AnswerList();
                activations.Load();
                answers.Load();
                Reload();
                Session["activations"] = activations; 
            }
            else
            {
                activations = (ActivationList)Session["activations"];
            }
        }

        protected void btnSubmitCode_Click(object sender, EventArgs e)
        {
            try
            {
                activations = new ActivationList();
                activation = new Activation();
                question = new Question();
                question.Answers = new AnswerList();

                activations.Load();

                var match = activations.FirstOrDefault(a => a.ActivationCode == txtCode.Text.ToLower());

                if (match != null)
                {
                    question.LoadQuestionByActivationCode(txtCode.Text);
                    lblQuestion.Text = question.Text;

                    Session["activations"] = activations;

                    Reload();

                    lblQuestion.Visible = true;
                    lblAnswer.Visible = true;
                    ddlAnswers.Visible = true;
                    btnSubmitAnswer.Visible = true;
                }
                else
                {
                    lblQuestion.Visible = false;
                    lblAnswer.Visible = false;
                    ddlAnswers.Visible = false;
                    btnSubmitAnswer.Visible = false;
                    throw new Exception("Please insert a valid code.");
                }
            }
            catch (Exception ex)
            {
                Response.Write("ERROR: " + ex.Message);
            }
        }

        protected void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                answer = question.Answers[ddlAnswers.SelectedIndex];

                if (answer.IsCorrect)
                {
                    lblQuestion.Text = "Correct answer!";
                    lblQuestion.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblQuestion.Text = "Wrong answer.";
                    lblQuestion.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Reload()
        {
            if (question != null && question.Answers.Count > 0)
            {
                ddlAnswers.DataSource = null;
                ddlAnswers.DataSource = question.Answers;
                ddlAnswers.DataTextField = "Text";
                ddlAnswers.DataValueField = "Id";
                ddlAnswers.DataBind();
            }
            else
            {
                ddlAnswers.DataSource = null;
                ddlAnswers.DataBind();
            }
        }

        /*
        private void LoadActivations()
        {
            try
            {
                HttpClient client = InitializeClient();

                string result;
                dynamic items;
                HttpResponseMessage response;

                //Call the API
                response = client.GetAsync("Activation").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Proces response
                    result = response.Content.ReadAsStringAsync().Result;

                    //Put json into the activation list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    activations = items.ToObject<ActivationList>();
                }
                else
                {
                    throw new Exception("Error: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {

                lblQuestion.Text = ex.Message;
            }
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://bjlsurveymaker.azurewebsites.net/api/");
            return client;
        }
        */
        
    }
}