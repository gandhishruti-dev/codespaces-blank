using MyWebApi.Interfaces;
using MyWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.Json;

namespace MyWebApi.Services
{
    public class PatientDataFromFileService : IPatientService
    {
       //private List<Patient> _patients;

        public PatientDataFromFileService()
        {
            //_patients = LoadPatientsFromFile();
        }

        public async Task<Patient?> GetPatient(int id)
        {
            List<Patient> _patients = LoadPatientsFromFile();
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(patient);
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            List<Patient> _patients = LoadPatientsFromFile();
            var newId = _patients.Max(p => p.Id) + 1;
            var newPatient = new Patient
            {
                Id = newId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Gender = patient.Gender,
                Address = patient.Address,
                Diagnosis = patient.Diagnosis
            };
            _patients.Add(newPatient);
            SavePatientsToFile(_patients);
            return await Task.FromResult(newPatient);
        }
       

        public async Task<Patient?> UpdatePatient(int id, Patient patient)
        {
            List<Patient> _patients = LoadPatientsFromFile();
            var existingPatient = _patients.FirstOrDefault(p => p.Id == id);
            if (existingPatient != null)
            {
                _patients.Remove(existingPatient);
                var updatedPatient = new Patient
                {
                    Id = id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Address = patient.Address,
                    Diagnosis = patient.Diagnosis
                };
                _patients.Add(updatedPatient);
                SavePatientsToFile(_patients);
                return await Task.FromResult(updatedPatient);
            }
            return null;
        }

        
        public async Task<bool> DeletePatient(int id)
        {
            List<Patient> _patients = LoadPatientsFromFile();
            var patientToDelete = _patients.FirstOrDefault(p => p.Id == id);
            if (patientToDelete != null)
            {
                _patients.Remove(patientToDelete);
                SavePatientsToFile(_patients);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);        
        }   

        #region Private helper methods for file operations

        private List<Patient> LoadPatientsFromFile()
        {
            List<Patient> patientsFromFile = new List<Patient>();
            string filePath = "Data/patients.json";
            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                patientsFromFile = JsonSerializer.Deserialize<List<Patient>>(jsonContent) ?? new List<Patient>();
            }
            
            return patientsFromFile;    
        }

        private void SavePatientsToFile(List<Patient> patients)
        {
            string filePath = "Data/patients.json";
            string jsonContent = JsonSerializer.Serialize(patients, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonContent);
        }

        #endregion
    }

    
}