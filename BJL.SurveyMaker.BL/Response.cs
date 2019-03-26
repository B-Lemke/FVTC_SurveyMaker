using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJL.SurveyMaker.PL;

namespace BJL.SurveyMaker.BL
{
    public class Response
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }


        public void LoadById()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //If the Id is set, get the result in the table where it matches
                    if (this.Id != Guid.Empty)
                    {
                        tblResponse response = dc.tblResponses.FirstOrDefault(r => r.Id == this.Id);

                        //If a row was retrieved, change 
                        if (response != null)
                        {
                            this.Id = response.Id;
                            this.QuestionId = response.QuestionId;
                            this.AnswerId = response.AnswerId;

                        }
                        else
                        {
                            throw new Exception("Could not find Response row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Response");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        public int Insert()
        {
            int result = 0;
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Answer new answer and set properties
                    tblResponse response = new tblResponse();
                    response.Id = Guid.NewGuid();
                    response.QuestionId = this.QuestionId;
                    response.AnswerId = this.AnswerId;


                    this.Id = response.Id; 

                    //Add answer to the table
                    dc.tblResponses.Add(response);

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
                        tblResponse response = dc.tblResponses.FirstOrDefault(r => r.Id == this.Id);

                        //If a row was retrieved, change 
                        if (response != null)
                        {
                            response.QuestionId = this.QuestionId;
                            response.AnswerId = this.AnswerId;

                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Response row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Response");
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
                        tblResponse response = dc.tblResponses.FirstOrDefault(r => r.Id == this.Id);

                        //If a row was retrieved, change 
                        if (response != null)
                        {
                            dc.tblResponses.Remove(response);


                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Response row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Response");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class ResponseList : List<Response>
    {
        public void Load()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Foreach answer in the database answer a new answer and add it to the answer list
                    dc.tblResponses.OrderBy(r => r.QuestionId).ToList()
                                   .ForEach(r => this.Add(new Response { Id = r.Id, QuestionId = r.QuestionId, AnswerId = r.AnswerId  }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
