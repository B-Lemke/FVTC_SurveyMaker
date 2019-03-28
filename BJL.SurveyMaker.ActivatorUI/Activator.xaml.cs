using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BJL.SurveyMaker.SL;
using BJL.SurveyMaker.BL;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BJL.SurveyMaker.ActivatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuestionList questions;
        ActivationList activations;

        public MainWindow()
        {
            InitializeComponent();
            questions = new QuestionList();
            LoadQuestions();
            Rebind();
        }


        private void LoadQuestions()
        {
            try
            {
                HttpClient client = InitializeClient();

                string result;
                dynamic items;
                HttpResponseMessage response;

                //Call the API
                response = client.GetAsync("Question").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Proces response
                    result = response.Content.ReadAsStringAsync().Result;

                    //Put json into the question list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    questions = items.ToObject<QuestionList>();
                }
                else
                {
                    throw new Exception("Error: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {

                lblStatus.Content = ex.Message;
            }
        }

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

                lblStatus.Content = ex.Message;
            }
        }


        private void Rebind()
        {
            cboQuestions.ItemsSource = null;
            cboQuestions.ItemsSource = questions;
            cboQuestions.DisplayMemberPath = "Text";
            cboQuestions.SelectedValuePath = "Id";
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://bjlsurveymaker.azurewebsites.net/api/");
            return client;
        }

        private void CboQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Clear status strip
                lblStatus.Content = String.Empty;


                activations = new ActivationList();
                LoadActivations();

                Activation activation = new Activation();
                activation = activations.FirstOrDefault(a => a.QuestionId == questions[cboQuestions.SelectedIndex].Id);

                if (activation != null)
                {
                    //Activation found
                    dtpEndDate.SelectedDate = activation.EndDate;
                    dtpStartDate.SelectedDate = activation.StartDate;
                    txtActivationCode.Text = activation.ActivationCode;
                }
                else
                {
                    dtpEndDate.SelectedDate = null;
                    dtpStartDate.SelectedDate = null;
                    txtActivationCode.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Remove the entry from the database via the SL
                activations = new ActivationList();
                LoadActivations();

                Activation activation = new Activation();
                activation = activations.FirstOrDefault(a => a.QuestionId == questions[cboQuestions.SelectedIndex].Id);

                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Activation/" + activation.Id).Result;

                //Update the screen
                dtpEndDate.SelectedDate = null;
                dtpStartDate.SelectedDate = null;
                txtActivationCode.Text = String.Empty;
                lblStatus.Content = "Activation Removed";

            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TO DO: VALIDATE THE USER DATA
                bool validData = true;
                activations = new ActivationList();
                LoadActivations();


                string activationcode = txtActivationCode.Text.ToLower();
                if (activationcode.Length != 5)
                {
                    validData = false;
                    throw new Exception("Activation code must be 5 characters.");
                }
                else if (activations.Any(a => a.ActivationCode == activationcode))
                {
                    validData = false;
                    throw new Exception("This activation code is already in use.");
                }
                else if (dtpEndDate.SelectedDate == null)
                {
                    validData = false;
                    throw new Exception("You must select an End Date.");
                }
                else if (dtpStartDate.SelectedDate == null)
                {
                    validData = false;
                    throw new Exception("You must select an Start Date.");
                }



                if (validData)
                {
                    SaveActivation();
                }

                else
                {
                    throw new Exception("Invalid data.");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }

        private void SaveActivation()
        {
            //Load the current activation for the question. This will tell us if there is already one in the database or if it needs to be creates
            HttpClient client = InitializeClient();

            Question question = questions[cboQuestions.SelectedIndex];
            HttpResponseMessage responseRetrieved = client.GetAsync("Activation?questionId=" + question.Id).Result;

            string result = responseRetrieved.Content.ReadAsStringAsync().Result;
            Activation activation = new Activation();
            activation = JsonConvert.DeserializeObject<Activation>(result);


            //Set the properties on the activation
            activation.QuestionId = questions[cboQuestions.SelectedIndex].Id;
            activation.StartDate = dtpStartDate.SelectedDate.Value;
            activation.EndDate = dtpEndDate.SelectedDate.Value;
            activation.ActivationCode = txtActivationCode.Text.ToLower();

            //Serialize and set headers
            string serializedActivation = JsonConvert.SerializeObject(activation);
            var activationContent = new StringContent(serializedActivation);
            activationContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            if (activation.Id == Guid.Empty)
            {
                //No activation record retrieved for this question, create one

                HttpResponseMessage response = client.PostAsync("Activation/", activationContent).Result;
                lblStatus.Content = "Activation Added to the Question";
            }
            else
            {
                //Activation retrieved, update it
                HttpResponseMessage response = client.PutAsync("Activation/" + activation.Id, activationContent).Result;
                lblStatus.Content = "Activation Updated on the Question";
            }

        }
    }
}
