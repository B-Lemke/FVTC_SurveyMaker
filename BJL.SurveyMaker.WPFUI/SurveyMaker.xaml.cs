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
using BJL.SurveyMaker.BL;

namespace BJL.SurveyMaker.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuestionList questions;
        AnswerList answers;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                questions = new QuestionList();
                questions.LoadQuestions();


                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }



        private void LoadComboBoxes()
        {
            //Clear out status label
            lblStatus.Content = String.Empty;

            //Load all questions and set to the combo boxes
            cboQuestion.ItemsSource = null;
            cboQuestion.ItemsSource = questions;
            cboQuestion.DisplayMemberPath = "Text";
            cboQuestion.SelectedValuePath = "Id";

            //Load all answers and set to the combo boxes
            answers = new AnswerList();
            answers.Load();

            cboCorrectAnswer.ItemsSource = null;
            cboCorrectAnswer.ItemsSource = answers;
            cboCorrectAnswer.DisplayMemberPath = "Text";
            cboCorrectAnswer.SelectedValuePath = "Id";

            cboWrongAnswer1.ItemsSource = null;
            cboWrongAnswer1.ItemsSource = answers;
            cboWrongAnswer1.DisplayMemberPath = "Text";
            cboWrongAnswer1.SelectedValuePath = "Id";

            cboWrongAnswer2.ItemsSource = null;
            cboWrongAnswer2.ItemsSource = answers;
            cboWrongAnswer2.DisplayMemberPath = "Text";
            cboWrongAnswer2.SelectedValuePath = "Id";

            cboWrongAnswer3.ItemsSource = null;
            cboWrongAnswer3.ItemsSource = answers;
            cboWrongAnswer3.DisplayMemberPath = "Text";
            cboWrongAnswer3.SelectedValuePath = "Id";

            cboQuestion.SelectedIndex = 0;
        }

        private void BtnManageAnswers_Click(object sender, RoutedEventArgs e)
        {
            ManageQAs manageQAs = new ManageQAs(QAMode.Answer);
            manageQAs.ShowDialog();
        }

        private void BtnManageQuestions_Click(object sender, RoutedEventArgs e)
        {
            ManageQAs manageQAs = new ManageQAs(QAMode.Question);
            manageQAs.ShowDialog();
        }

        private void QuestionSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Question was picked, find its answers
                Question question = new Question();

                if (cboQuestion.SelectedItem != null)
                {


                    question = questions.ElementAt(cboQuestion.SelectedIndex);

                    //Clear out all selections
                    cboCorrectAnswer.SelectedItem = null;
                    cboWrongAnswer1.SelectedItem = null;
                    cboWrongAnswer2.SelectedItem = null;
                    cboWrongAnswer3.SelectedItem = null;

                    int wrongAnswersInserted = 0;

                    //Loop through each answer and select it in a combo box
                    foreach (Answer a in question.Answers)
                    {
                        if (a.IsCorrect)
                        {
                            cboCorrectAnswer.SelectedIndex = answers.FindIndex(ans => ans.Id == a.Id);
                        }
                        else
                        {
                            switch (wrongAnswersInserted)
                            {
                                case 0:
                                    cboWrongAnswer1.SelectedIndex = answers.FindIndex(ans => ans.Id == a.Id);
                                    break;
                                case 1:
                                    cboWrongAnswer2.SelectedIndex = answers.FindIndex(ans => ans.Id == a.Id);
                                    break;
                                case 2:
                                    cboWrongAnswer3.SelectedIndex = answers.FindIndex(ans => ans.Id == a.Id);
                                    break;
                                default:
                                    break;
                            }

                            wrongAnswersInserted++;
                        }
                    }
                }

                //Clear out status label
                lblStatus.Content = String.Empty;


            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }

        private void SurveyMakerActivated(object sender, EventArgs e)
        {
            try
            {
                //LoadComboBoxes();
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Question question = questions.ElementAt(cboQuestion.SelectedIndex);

                //Clear out the current answers on the question
                question.Answers.Clear();

                //Save the currently selected answers to the question's answer list
                if(cboCorrectAnswer.SelectedItem != null)
                {
                    //Check to make sure that duplicate answers aren't selected
                    CheckSameSelection(cboCorrectAnswer, cboWrongAnswer1);
                    CheckSameSelection(cboCorrectAnswer, cboWrongAnswer2);
                    CheckSameSelection(cboCorrectAnswer, cboWrongAnswer3);

                    //Find the answer in the master answer list and add a copy to this question's answer list
                    Answer masterAnswer = answers.ElementAt(cboCorrectAnswer.SelectedIndex);
                    Answer newAnswer = new Answer { Id = masterAnswer.Id, Text = masterAnswer.Text, IsCorrect = true };
                    question.Answers.Add(newAnswer);

                    newAnswer = null;
                }
                if (cboWrongAnswer1.SelectedItem != null)
                {
                    CheckSameSelection(cboWrongAnswer1, cboWrongAnswer2);
                    CheckSameSelection(cboWrongAnswer1, cboWrongAnswer3);



                    //Find the answer in the master answer list and add a copy to this question's answer list
                    Answer masterAnswer = answers.ElementAt(cboWrongAnswer1.SelectedIndex);
                    Answer newAnswer = new Answer { Id = masterAnswer.Id, Text = masterAnswer.Text, IsCorrect = false };
                    question.Answers.Add(newAnswer);

                    newAnswer = null;
                }
                if (cboWrongAnswer2.SelectedItem != null)
                {
                    CheckSameSelection(cboWrongAnswer2, cboWrongAnswer3);


                    //Find the answer in the master answer list and add a copy to this question's answer list
                    Answer masterAnswer = answers.ElementAt(cboWrongAnswer2.SelectedIndex);
                    Answer newAnswer = new Answer { Id = masterAnswer.Id, Text = masterAnswer.Text, IsCorrect = false };
                    question.Answers.Add(newAnswer);

                    newAnswer = null;
                }
                if (cboWrongAnswer3.SelectedItem != null)
                {

                    //Find the answer in the master answer list and add a copy to this question's answer list
                    Answer masterAnswer = answers.ElementAt(cboWrongAnswer3.SelectedIndex);
                    Answer newAnswer = new Answer { Id = masterAnswer.Id, Text = masterAnswer.Text, IsCorrect = false };
                    question.Answers.Add(newAnswer);

                    newAnswer = null;
                }
                //Save the question answers to the database
                question.SaveAnswers();

                //update the status label
                lblStatus.Content = "Saved: Updated the answers on the question!.";


            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }

            
        }

        private void CheckSameSelection(ComboBox cbo1, ComboBox cbo2)
        {
            if (cbo1.SelectedItem == cbo2.SelectedItem)
            {
                string cbo1Display;
                string cbo2Display;

                switch (cbo1.Name)
                {
                    case "cboCorrectAnswer":
                        cbo1Display = "The correct answer";
                        break;
                    case "cboWrongAnswer1":
                        cbo1Display = "The first wrong answer";
                        break;
                    case "cboWrongAnswer2":
                        cbo1Display = "The second wrong answer";
                        break;
                    default:
                        cbo1Display = String.Empty;
                        break;
                }
                switch (cbo2.Name)
                {
                    case "cboWrongAnswer1":
                        cbo2Display = "the first wrong answer";
                        break;
                    case "cboWrongAnswer2":
                        cbo2Display = "the second wrong answer";
                        break;
                    case "cboWrongAnswer3":
                        cbo2Display = "the third wrong answer";
                        break;
                    default:
                        cbo2Display = String.Empty;
                        break;
                }

                throw new Exception("Error: " +cbo1Display + " and " + cbo2Display + " are the same.");
            }
        }

        private void btnRemoveAnswersClicked(object sender, RoutedEventArgs e)
        {
            cboCorrectAnswer.SelectedItem = null;
            cboWrongAnswer1.SelectedItem = null;
            cboWrongAnswer2.SelectedItem = null;
            cboWrongAnswer3.SelectedItem = null; 
        }

        private void cboCorrectAnswerChanged(object sender, SelectionChangedEventArgs e)
        {
            AnswerChanged();
        }

        private void AnswerChanged()
        {
            lblStatus.Content = "Unsaved Changes!";
        }

        private void cboWrongAnswer1Changed(object sender, SelectionChangedEventArgs e)
        {
            AnswerChanged();
        }

        private void cboWrongAnswer2Changed(object sender, SelectionChangedEventArgs e)
        {
            AnswerChanged();
        }

        private void cboWrongAnswer3Changed(object sender, SelectionChangedEventArgs e)
        {
            AnswerChanged();
        }

        private void BtnCorrectAnswerClear_Click(object sender, RoutedEventArgs e)
        {
            cboCorrectAnswer.SelectedItem = null;
        }

        private void BtnWrongAnswer1Clear_Click(object sender, RoutedEventArgs e)
        {
            cboWrongAnswer1.SelectedItem = null;
        }

        private void BtnWrongAnswer2Clear_Click(object sender, RoutedEventArgs e)
        {
            cboWrongAnswer2.SelectedItem = null;
        }

        private void BtnWrongAnswer3Clear_Click(object sender, RoutedEventArgs e)
        {
            cboWrongAnswer3.SelectedItem = null;
        }
    }
}
