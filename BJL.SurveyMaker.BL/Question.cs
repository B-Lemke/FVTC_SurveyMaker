using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJL.SurveyMaker.PL;

namespace BJL.SurveyMaker.BL
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public AnswerList Answers { get; set; }

        public int InsertQuestion()
        {
            int result = 0;
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Question new question and set properties
                    tblQuestion question = new tblQuestion();
                    question.Id = Guid.NewGuid();
                    question.Text = this.Text;

                    this.Id = question.Id;

                    //Add question to the table
                    dc.tblQuestions.Add(question);

                    //Commit the changes
                    result = dc.SaveChanges();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateQuestion()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblQuestion question = dc.tblQuestions.Where(q => q.Id == this.Id).FirstOrDefault();

                        //If q row was retrieved, change 
                        if (question != null)
                        {
                            question.Text = this.Text;

                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Question row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Question");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteQuestion()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblQuestion question = dc.tblQuestions.Where(q => q.Id == this.Id).FirstOrDefault();

                        //If q row was retrieved, change 
                        if (question != null)
                        {
                            dc.tblQuestions.Remove(question);

                            //Make sure to retrieve any rows from the questionAnswerstable with this questionID and delete them as well
                            /*var questionAnswers = dc.tblQuestionAnswers.Where(qa => qa.QuestionId == this.Id);
                            foreach (tblQuestionAnswer qa in questionAnswers)
                            {
                                dc.tblQuestionAnswers.Remove(qa);
                            }
                            */


                            //Use stored procedure to retrieve rows from the questionAnswers table with the questionId and delete them
                            dc.spDeleteQAWithQuestion(question.Id);


                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Question row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Question");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadQuestionById()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblQuestion question = dc.tblQuestions.FirstOrDefault(q => q.Id == this.Id);

                        //If q row was retrieved, change 
                        if (question != null)
                        {
                            this.Id = question.Id;
                            this.Text = question.Text;

                            //Load the answers
                            this.LoadAnswers();
                        }
                        else
                        {
                            throw new Exception("Could not find Question row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Question");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadQuestionByActivationCode(string activationCode)
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches

                    var results = from q in dc.tblQuestions
                                  join a in dc.tblActivations on q.Id equals a.QuestionId
                                  where a.ActivationCode == activationCode
                                  //&& a.StartDate < DateTime.Now
                                  //&& a.EndDate > DateTime.Now
                                  select new
                                  {
                                      q.Id,
                                      q.Text
                                  };

                    //If q row was retrieved, change 
                    if (results.Any())
                    {

                        this.Id = results.FirstOrDefault().Id;
                        this.Text = results.FirstOrDefault().Text;

                        //Load the answers
                        this.LoadAnswers();
                    }
                    else
                    {
                        this.Id = Guid.Empty;
                        this.Text = "Non-Valid Activation Code";
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveAnswers()
        {
            //Delete all current questionanswers in the database for this question and save the current ones

            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {

                        //get all of the questionanswers with this questionId
                        var questionAnswers = dc.tblQuestionAnswers.Where(qa => qa.QuestionId == this.Id);

                        //Delete the questionanswers with this questionId from the database
                        foreach (tblQuestionAnswer qa in questionAnswers)
                        {
                            dc.tblQuestionAnswers.Remove(qa);
                        }

                        //Foreach question answer on the AnswerList, put it in QuestionAnswers with this question ID
                        foreach (Answer a in Answers)
                        {
                            //set properties
                            tblQuestionAnswer questionAnswer = new tblQuestionAnswer();
                            questionAnswer.AnswerId = a.Id;
                            questionAnswer.QuestionId = this.Id;
                            questionAnswer.IsCorrect = a.IsCorrect;
                            questionAnswer.Id = Guid.NewGuid();

                            //add to the tblQuestionAnswers
                            dc.tblQuestionAnswers.Add(questionAnswer);
                        }

                        //Return the number of rows affected
                        return dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Id not set on Question");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int LoadAnswers()
        {
            //Load all of the current questionanswers from the database for this question
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        //Instantiate the answer list
                        Answers = new AnswerList();

                        //get all of the questionanswers with this questionId
                        var questionAnswers = dc.tblQuestionAnswers.Where(qa => qa.QuestionId == this.Id);

                        //Foreach question answer, get the answer and put it in Answers
                        foreach (tblQuestionAnswer qa in questionAnswers)
                        {
                            Answer answer = new Answer();
                            answer.Id = qa.AnswerId;
                            answer.LoadById();

                            //Set the correct value
                            if (qa.IsCorrect)
                            {
                                answer.IsCorrect = true;
                            }

                            Answers.Add(answer);
                        }

                        dc.SaveChanges();

                        //Return the new length of the answers list
                        return Answers.Count;
                    }
                    else
                    {
                        throw new Exception("Id not set on Question");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }

    public class QuestionList : List<Question>
    {
        public void LoadQuestions()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Foreach question in the database question q new question and add it to the question list
                    dc.tblQuestions.OrderBy(q => q.Text).ToList().ForEach(q => this.Add(new Question { Id = q.Id, Text = q.Text }));

                    foreach (Question q in this)
                    {
                        q.LoadAnswers();
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
