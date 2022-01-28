using DomainLayer.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.IMasterService
{
    public interface IMasterService
    {

        List<Allergy> GetAllAllergydetails();
        List<Allergy> GetAllergyfromallergytype(string AllergyType);
        Diagnosis Getdetailsfromdiagnosisdes(string diagnosisisdes);
        List<Diagnosis> Getdiagnosisdetails();
        Procedure Getdetailsfromproceduredes(string diagnosisisdes);
        List<Procedure> Getproceduredetails();
        Drug Getdetailsfromdrugname(string drugname);
        List<Drug> Getdrugdetails();
    }
}
