using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BJL.SurveyMaker.BL;

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
            activations = new ActivationList();
            activations.Load();
            Session["activations"] = activations;
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
    }
}