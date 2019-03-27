using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BJL.SurveyMaker.PL;

namespace BJL.SurveyMaker.BL
{
    public class Activation
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ActivationCode { get; set; }





        public int Insert()
        {
            int result = 0;
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Activation new activation and set properties
                    tblActivation activation = new tblActivation();
                    activation.Id = Guid.NewGuid();
                    activation.EndDate = this.EndDate;
                    activation.StartDate = this.StartDate;
                    activation.QuestionId = this.QuestionId;
                    activation.ActivationCode = this.ActivationCode;

                    this.Id = activation.Id;

                    //Add activation to the table
                    dc.tblActivations.Add(activation);

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
                        tblActivation activation = dc.tblActivations.Where(a => a.Id == this.Id).FirstOrDefault();

                        //If a row was retrieved, change 
                        if (activation != null)
                        {
                            activation.ActivationCode = this.ActivationCode;
                            activation.EndDate = this.EndDate;
                            activation.StartDate = this.StartDate;
                            activation.QuestionId = this.QuestionId;

                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Activation row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Activation");
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
                        tblActivation activation = dc.tblActivations.Where(a => a.Id == this.Id).FirstOrDefault();

                        //If a row was retrieved, change 
                        if (activation != null)
                        {
                            dc.tblActivations.Remove(activation);

                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Could not find Activation row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Activation");
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
                        tblActivation activation = dc.tblActivations.FirstOrDefault(a => a.Id == this.Id);

                        //If a row was retrieved, change 
                        if (activation != null)
                        {
                            this.Id = activation.Id;
                            this.EndDate = activation.EndDate;
                            this.StartDate = activation.StartDate;
                            this.ActivationCode = activation.ActivationCode;
                            this.QuestionId = activation.QuestionId;

                        }
                        else
                        {
                            throw new Exception("Could not find Activation row with this ID");
                        }
                    }
                    else
                    {
                        throw new Exception("Id not set on Activation");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

    public class ActivationList : List<Activation>
    {
        public void Load()
        {
            try
            {
                using (SurveyEntities dc = new SurveyEntities())
                {
                    //Foreach activation in the database activation a new activation and add it to the activation list
                    dc.tblActivations.OrderBy(a => a.ActivationCode).ToList().ForEach(a => this
                                    .Add(new Activation { Id = a.Id, ActivationCode = a.ActivationCode, EndDate = a.EndDate, QuestionId = a.QuestionId, StartDate = a.StartDate }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
