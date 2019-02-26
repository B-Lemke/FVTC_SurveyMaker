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
using System.Windows.Shapes;
using BJL.SurveyMaker.BL;


namespace BJL.SurveyMaker.WPFUI
{
    public enum QAMode
    {
        Answer = 0,
        Question = 1
    }

    /// <summary>
    /// Interaction logic for AddQuestionOrAnswer.xaml
    /// </summary>
    public partial class ManageQAs : Window
    {
        QAMode qaMode;
        QuestionList questions;
        AnswerList answers;

        public ManageQAs(QAMode mode)
        {
            InitializeComponent();

            //Adjust labels and title for the mode
            lblQOrA.Content = mode.ToString() + "s:";
            Title = "Manage " + mode.ToString() + "s";

            //save the mode in a modular level variable
            qaMode = mode;

            //fill the combobox
            switch (mode)
            {
                case QAMode.Answer:
                    answers = new AnswerList();
                    answers.Load();

                    cboQorAs.ItemsSource = null;
                    cboQorAs.ItemsSource = answers;
                    break;

                case QAMode.Question:
                    questions = new QuestionList();
                    questions.LoadQuestions();

                    cboQorAs.ItemsSource = null;
                    cboQorAs.ItemsSource = questions;
                    break;
                default:
                    break;
            }


            cboQorAs.DisplayMemberPath = "Text";
            cboQorAs.SelectedValuePath = "Id";


        }

        private void cboQorAChanged(object sender, SelectionChangedEventArgs e)
        {
            //Copy the value of the 
            switch (qaMode)
            {
                case QAMode.Answer:
                    txtText.Text = answers.ElementAt(cboQorAs.SelectedIndex).Text;
                    break;
                case QAMode.Question:
                    txtText.Text = questions.ElementAt(cboQorAs.SelectedIndex).Text;
                    break;
                default:
                    break;
            }
            
        }


    }
}
