using MyWebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Interfaces
{
     public interface IPatientService
    {
        // Define methods for patient management, e.g.:
        Task<List<Patient>> GetPatients();
        Task<Patient?> GetPatient(int id);
        Task<Patient> CreatePatient(Patient patient);
        Task<Patient?> UpdatePatient(int id, Patient patient);
        Task<bool> DeletePatient(int id);
    }
}       