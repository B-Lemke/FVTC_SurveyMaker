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
                    dc.tblQuestions.ToList().ForEach(q => this.Add(new Question { Id = q.Id, Text = q.Text }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
