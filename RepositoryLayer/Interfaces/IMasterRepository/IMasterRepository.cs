using DomainLayer.Models;
using DomainLayer.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.IMasterRepository
{
    public interface IMasterRepository
    {
        List<Allergy> GetAllergydetails();
        List<Allergy> GetAllergyfromallergytype(string AllergyType);
        Diagnosis Getdignosisdetailsfromdesc(string diagnosisisdes);
        List<Diagnosis> Getdignosisdetails();
        Drug Getdetailsfromdrugname(string drugname);
        Procedure Getdetailsfromproceduredes(string diagnosisisdes);
        List<Drug> Getdrugdetail();
        List<Procedure> Getproceduredetails();
        string GetRolefromid(string id);
    }
}
