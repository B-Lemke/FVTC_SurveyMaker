using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJL.SurveyMaker.PL;

namespace BJL.SurveyMaker.BL
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public int Insert()
        {
            int result = 0;
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Answer new answer and set properties
                    tblAnswer answer = new tblAnswer();
                    answer.Id = Guid.NewGuid();
                    answer.Text = this.Text;

                    this.Id = answer.Id;

                    //Add answer to the table
                    dc.tblAnswers.Add(answer);

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

        public int Update()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblAnswer answer = dc.tblAnswers.Where(a => a.Id == this.Id).FirstOrDefault();

                        //If a row was retrieved, change 
                        if (answer != null)
                        {
                            answer.Text = this.Text;

                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Answer row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Answer");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblAnswer answer = dc.tblAnswers.Where(a => a.Id == this.Id).FirstOrDefault();

                        //If a row was retrieved, change 
                        if (answer != null)
                        {
                            dc.tblAnswers.Remove(answer);

                            //Make sure to retrieve any rows from the questionAnswerstable with this answerID and delete them as well
                            /*var questionAnswers = dc.tblQuestionAnswers.Where(qa => qa.AnswerId == this.Id);
                            foreach(tblQuestionAnswer qa in questionAnswers)
                            {
                                dc.tblQuestionAnswers.Remove(qa);
                            }
                            */

                            //Use stored procedure to retrieve rows from the questionAnswers table with the answerId and delete them
                            dc.spDeleteQAWithAnswer(answer.Id);

                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Answer row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Answer");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadById()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblAnswer answer = dc.tblAnswers.FirstOrDefault(a => a.Id == this.Id);

                        //If a row was retrieved, change 
                        if (answer != null)
                        {
                            this.Id = answer.Id;
                            this.Text = answer.Text;

                        }
                        else
                        {
                            throw new Exception("Could not find Answer row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Answer");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class AnswerList : List<Answer>
    {
        public void Load()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Foreach answer in the database answer a new answer and add it to the answer list
                    dc.tblAnswers.OrderBy(a => a.Text).ToList().ForEach(a => this.Add(new Answer { Id = a.Id, Text = a.Text }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
