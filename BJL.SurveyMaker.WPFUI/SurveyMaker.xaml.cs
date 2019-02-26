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

                LoadComboBoxes();
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }



        private void LoadComboBoxes()
        {
            //Load all questions and set to the combo boxes
            questions = new QuestionList();
            questions.LoadQuestions();

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
                question = questions.ElementAt(cboQuestion.SelectedIndex);

                int wrongAnswersInserted = 0;

                //Loop through each answer and select it in a combo box
                foreach(Answer a in question.Answers)
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
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }
    }
}
