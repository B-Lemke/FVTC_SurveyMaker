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

        private enum EditMode
        {
            Update = 0,
            Add = 1
        }


        QAMode qaMode;
        EditMode editMode;
        QuestionList questions;
        AnswerList answers;



        public ManageQAs(QAMode mode)
        {
            try
            {
                InitializeComponent();

                //Adjust labels and title for the mode
                lblQOrA.Content = mode.ToString() + "s:";
                Title = "Manage " + mode.ToString() + "s";

                //save the mode in a modular level variable
                qaMode = mode;

                //Instantiate the proper list
                switch (mode)
                {
                    case QAMode.Answer:
                        answers = new AnswerList();
                        answers.Load();
                        break;

                    case QAMode.Question:
                        questions = new QuestionList();
                        questions.LoadQuestions();

                        break;
                    default:
                        break;
                }

                rebindComboBox(mode);

            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }

        private void rebindComboBox(QAMode mode)
        {
            //rebind the combobox
            switch (mode)
            {
                case QAMode.Answer:

                    cboQorAs.ItemsSource = null;
                    cboQorAs.ItemsSource = answers;
                    break;

                case QAMode.Question:

                    cboQorAs.ItemsSource = null;
                    cboQorAs.ItemsSource = questions;
                    break;
                default:
                    break;
            }

            cboQorAs.DisplayMemberPath = "Text";
            cboQorAs.SelectedValuePath = "Id";


            //Update the edit button
            btnSave.Content = "Add " + qaMode.ToString();
            editMode = EditMode.Add;
        }

        private void cboQorAChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboQorAs.SelectedItem != null)
                {
                    //Copy the value of the cboBox into the textbox for editing
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


                    //Update the edit button
                    btnSave.Content = "Update " + qaMode.ToString();
                    editMode = EditMode.Update;
                }
                else
                {
                    //If null clear the textbox and update the edit button to read add and update the editMode
                    txtText.Text = String.Empty;
                    btnSave.Content = "Add " + qaMode.ToString();
                    editMode = EditMode.Add;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }

        }




        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            //Select nothing in the comboBox
            cboQorAs.SelectedItem = null;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if the cboBox is not selecting anything
                if (cboQorAs.SelectedItem == null)
                {
                    throw new Exception("You must select a(n) " + qaMode.ToString() + " to delete");
                }
                else
                {
                    if (qaMode == QAMode.Answer)
                    {
                        //Delete the Answer
                        Answer answer = new Answer();
                        answer = answers.ElementAt(cboQorAs.SelectedIndex);

                        int result = answer.Delete();

                        if (result > 0)
                        {
                            lblStatus.Content = "Removed Answer: " + answer.Text;
                        }
                        else
                        {
                            throw new Exception("Answer not deleted");
                        }

                        //Remove from the answer list
                        answers.Remove(answer);

                        //Rebind the combobox
                        rebindComboBox(qaMode);
                    }
                    else
                    {
                        //Delete the Question
                        Question question = new Question();
                        question = questions.ElementAt(cboQorAs.SelectedIndex);

                        int result = question.DeleteQuestion();

                        if (result > 0)
                        {
                            lblStatus.Content = "Removed Question: " + question.Text;
                        }
                        else
                        {
                            throw new Exception("Question not deleted");
                        }

                        //Remove from the answer list
                        questions.Remove(question);

                        //Rebind the combobox
                        rebindComboBox(qaMode);
                    }
                }

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
                int result;

                //Check whether adding a new question or updating a question
                switch (editMode)
                {
                    /////////////////Editing a question/answer
                    case EditMode.Update:
                        //Check if question of answer
                        if (txtText.Text != String.Empty)
                        {
                            switch (qaMode)
                            {
                                case QAMode.Answer:
                                    Answer answer = answers.ElementAt(cboQorAs.SelectedIndex);
                                    answer.Text = txtText.Text;
                                    result = answer.Update();
                                    if (result > 0)
                                    {
                                        lblStatus.Content = "Updated Answer: " + answer.Text;
                                    }
                                    else
                                    {
                                        throw new Exception("Answer not updated");
                                    }


                                    break;
                                case QAMode.Question:
                                    Question question = questions.ElementAt(cboQorAs.SelectedIndex);
                                    question.Text = txtText.Text;
                                    result = question.UpdateQuestion();
                                    if (result > 0)
                                    {
                                        lblStatus.Content = "Updated Question: " + question.Text;
                                    }
                                    else
                                    {
                                        throw new Exception("Question not updated");
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                        else {
                            throw new Exception("Did you mean to delete this " + qaMode.ToString() + "? Please use the delete button instead.");
                        }

                        rebindComboBox(qaMode);
                        break;







                    ////////////////////////Adding a question/answer
                    case EditMode.Add:
                        //Check if question of answer
                        if (txtText.Text != String.Empty)
                        {
                            switch (qaMode)
                            {
                                case QAMode.Answer:
                                    //Create a new answer and put it in the database
                                    Answer answer = new Answer();
                                    answer.Text = txtText.Text;
                                    answers.Add(answer);
                                    result = answer.Insert();

                                    //Update the label with success or failure message
                                    if (result > 0)
                                    {
                                        lblStatus.Content = "Added Answer: " + answer.Text;
                                    }
                                    else
                                    {
                                        throw new Exception("Answer not inserted");
                                    }


                                    break;
                                case QAMode.Question:
                                    //Create question and add to the DB
                                    Question question = new Question();
                                    question.Text = txtText.Text;
                                    questions.Add(question);
                                    result = question.InsertQuestion();
                                    //Show result on screen
                                    if (result > 0)
                                    {
                                        lblStatus.Content = "Added Question: " + question.Text;
                                    }
                                    else
                                    {
                                        throw new Exception("Question not inserted");
                                    }

                                    break;
                                default:
                                    break;
                            }

                            //Clear the entered text
                            txtText.Text = String.Empty;
                        }
                        else
                        {
                            throw new Exception("You must enter a value to add");
                        }
                        break;
                    default:
                        break;

                }

                //Rebind the combobox
                rebindComboBox(qaMode);
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
            }
        }
    }
}
