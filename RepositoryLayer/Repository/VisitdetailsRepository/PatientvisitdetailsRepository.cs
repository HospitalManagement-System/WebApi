using DomainLayer.Models;
using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.VisitdetailsRepository
{
    public class PatientvisitdetailsRepository : IPatientvisitdetailRepository
    {
        private ApplicationDbContext _context;
        IInMemoryCache _memorycache;
        public int SaveResult { get; private set; }
        public string Result { get; set; }
        public PatientvisitdetailsRepository(IInMemoryCache memorycache, ApplicationDbContext context)
        {
            _context = context;
            _memorycache = memorycache;
        }

        public string AddpatientvisitDetails(PatientVisitDetails patientVisitDetails)
        {
            try
            {
                // var Id = new Guid(patientVisitDetails.AppointmentId.ToString());
                PatientVisitDetails patientDetailsList = _context.PatientVisitDetails.Where(x => x.AppointmentId == patientVisitDetails.AppointmentId).FirstOrDefault();
                if (patientDetailsList == null)
                {
                    var PatientVisitDetails = new PatientVisitDetails
                    {
                        Height = patientVisitDetails.Height,
                        Weight = patientVisitDetails.Weight,
                        BloodPressure = patientVisitDetails.BloodPressure,
                        BodyTemprature = patientVisitDetails.BodyTemprature,
                        RespirationRate = patientVisitDetails.RespirationRate,
                        DoctorDescription = patientVisitDetails.DoctorDescription,
                        ProcedureDesciption = patientVisitDetails.ProcedureDesciption,
                        DiagnosisDescription = patientVisitDetails.DiagnosisDescription,
                        AppointmentId = patientVisitDetails.AppointmentId,
                        Createddate = DateTime.Now,

                    };
                    _context.PatientVisitDetails.Add(PatientVisitDetails);



                    int result = _context.SaveChanges();
                    Result = (SaveResult == 1) ? "Failure" : "Success";
                }
                return Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public PatientVisitDetails Getdetailsfromid(string appointmentid)
        {
            try
            {

                var Id = new Guid(appointmentid);
                PatientVisitDetails patientDetailsList = _context.PatientVisitDetails.Where(x => x.AppointmentId == Id).FirstOrDefault();
                if (patientDetailsList != null)
                {
                    if (patientDetailsList.Equals(null))
                    {

                        patientDetailsList.Diagnosislist = patientDetailsList.DiagnosisDescription.Split(',').ToList();
                        patientDetailsList.Druglist = patientDetailsList.DrugDescription.Split(',').ToList();
                        patientDetailsList.Procedureslist = patientDetailsList.ProcedureDesciption.Split(',').ToList();
                    }
                }
                return patientDetailsList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        
        public string updatepatientvisitdetails(string id, PatientVisitDetails patientVisitDetails)
        { 
            var visitid = new Guid(id);
            try
            {
                PatientVisitDetails Existingdetails = _context.PatientVisitDetails.Where(x => x.Id == visitid).FirstOrDefault();

                if (Existingdetails != null)
                {

                    {
                        Existingdetails.Height = patientVisitDetails.Height;
                        Existingdetails.Weight = patientVisitDetails.Weight;
                        Existingdetails.BloodPressure = patientVisitDetails.BloodPressure;
                        Existingdetails.BodyTemprature = patientVisitDetails.BodyTemprature;
                        Existingdetails.RespirationRate = patientVisitDetails.RespirationRate;
                        Existingdetails.DiagnosisDescription =string.Join(",", patientVisitDetails.Diagnosislist.ToArray());
                       Existingdetails.ProcedureDesciption = string.Join(",", patientVisitDetails.Procedureslist.ToArray());
                        Existingdetails.DrugDescription = string.Join(",", patientVisitDetails.Druglist.ToArray());

                    };

                    _context.PatientVisitDetails.Update(Existingdetails);
                    int result = _context.SaveChanges();
                    Result = (SaveResult == 1) ? "Success" : "Failure";
                }





                return Result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public string PostAllocatedPatientDetails(AllocatedPatientDetails allocatedPatient)
        {
            try
            {
                PatientDemographicDetails Existingdetails = _context.PatientDemographicDetails.Where(x => x.Contact == allocatedPatient.Contact.ToString()).FirstOrDefault();
                bool isNew = true;
                if (Existingdetails != null)
                {
                    isNew = false;
                }
                else
                {
                    Existingdetails = new PatientDemographicDetails();
                }
                if (!isNew)
                {
                    // Old Patient                    
                    _context.PatientDemographicDetails.Update(Existingdetails);
                    int result = _context.SaveChanges();
                    return Existingdetails.PatientId.ToString();
                }
                else
                {
                    List<RoleMaster> lstRoleMaster = (List<RoleMaster>)_memorycache.GetCache<RoleMaster>("Rolemaster");
                    if (lstRoleMaster == null)
                    {
                        lstRoleMaster = _context.RoleMaster.ToList();
                        _memorycache.SetCache<RoleMaster>("Rolemaster", lstRoleMaster);
                    }
                    RoleMaster roleMaster = lstRoleMaster.Where(x => x.UserRole.ToUpper() == "Patient".ToUpper()).FirstOrDefault();
                    var UserDetails = new UserDetails
                    {
                        UserName = allocatedPatient.Contact.ToString(),
                        Password = allocatedPatient.Password,
                        Status = true,
                        IsFirstLogIn = true,
                        IsActive = true,
                        PatientDetails = new PatientDetails
                        {
                            Title = allocatedPatient.Title,
                            FirstName = allocatedPatient.FirstName,
                            LastName = allocatedPatient.LastName,
                            Contact = allocatedPatient.Contact,
                            IsActive = true,
                            PatientDemographicDetails = new PatientDemographicDetails
                            {
                                FirstName = allocatedPatient.FirstName,
                                LastName = allocatedPatient.LastName,
                                Contact = allocatedPatient.Contact.ToString(),
                                DateOfBirth = allocatedPatient.DateofBirth,
                                PatientRelativeDetails = new PatientRelativeDetails
                                {

                                }
                            },

                        },
                        RoleId = roleMaster.Id
                    };
                    _context.UserDetails.Add(UserDetails);
                    var Save = _context.SaveChanges();
                    PatientDetails patientDetails  = _context.PatientDetails.Where(x => x.UserId == UserDetails.Id).FirstOrDefault();
                    BedManagement bedDetails = _context.BedManagement.SingleOrDefault(i => i.Floor == allocatedPatient.BedDetails.Floor
                                    && i.Room == allocatedPatient.BedDetails.Room
                                    && i.Bed == allocatedPatient.BedDetails.Bed);
                    bedDetails.PatientId = patientDetails.Id;
                    _context.BedManagement.Update(bedDetails);
                    Save = _context.SaveChanges();
                    List<Products> productList = _context.Products.ToList();
                    EmployeeDetails physicianDetails = _context.EmployeeDetails.Where(x => x.Id.ToString() == allocatedPatient.physicianId).SingleOrDefault();
                   
                    double TotleCost = Convert.ToDouble(allocatedPatient.BedDetails.BedCost) + (double)physicianDetails.CostPerVisit;
                    BillInfo billInfo = new BillInfo();
                    billInfo.PatientId = patientDetails.Id;
                    billInfo.IsPaid = false;
                    billInfo.StartDateTime = new DateTime();
                    billInfo.EndDateTime = new DateTime();
                    billInfo.Balance = TotleCost;
                    billInfo.UserId = UserDetails.Id;
                    billInfo.BillPaid = 0;
                    _context.BillInfo.Add(billInfo);
                    Save = _context.SaveChanges();

                    // Bed Cost
                    PatientInOut patientInOut = new PatientInOut();
                    patientInOut.UserId = UserDetails.Id;
                    patientInOut.PatientId = patientDetails.Id;
                    patientInOut.ProductId = productList.SingleOrDefault(x => x.ProductName == "BedCost").Id;
                    patientInOut.Amount = Convert.ToDouble(allocatedPatient.BedDetails.BedCost);
                    patientInOut.DateOfProductAdded = new DateTime();
                    patientInOut.EmployeeId = physicianDetails.Id;
                    _context.PatientInOut.Add(patientInOut);
                    Save = _context.SaveChanges();
                    // Appoinement Charges
                    patientInOut = new PatientInOut();
                    patientInOut.UserId = UserDetails.Id;
                    patientInOut.PatientId = patientDetails.Id;
                    patientInOut.ProductId = productList.SingleOrDefault(x => x.ProductName == "Appointment").Id;
                    patientInOut.Amount = (double)physicianDetails.CostPerVisit;
                    patientInOut.DateOfProductAdded = new DateTime();
                    patientInOut.EmployeeId = physicianDetails.Id;
                    _context.PatientInOut.Add(patientInOut);
                    Save = _context.SaveChanges();                    
                    return patientDetails.Id.ToString();
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public IEnumerable<PatientVisitDetails> GetdetailsfromPatientid(string patientid)
        {
            try
            {
                var Id = new Guid(patientid);
                List<Appointments> lst = _context.Appointments.Where(x => x.PatientId == Id).ToList();
                var result = from visit in _context.PatientVisitDetails
                             join app in lst
                             on visit.AppointmentId equals app.Id
                             select new PatientVisitDetails
                             {
                                 Id = visit.Id
                             };

                //List < PatientVisitDetails > patientDetailsList = _context.PatientVisitDetails.Where(x => x.Id == Id).ToList();
                return result;
            }
            catch(Exception ex)
            {
                return null;

            }
        }
    }
    
}