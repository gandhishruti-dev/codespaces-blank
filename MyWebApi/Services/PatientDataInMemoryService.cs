using MyWebApi.Interfaces;
using MyWebApi.Model;
using MyWebApi.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MyWebApi.Services
{
    public class PatientDataInMemoryService : IPatientService
    {
        private readonly ConcurrentDictionary<int, Patient> _patients;

        public PatientDataInMemoryService()
        {
            _patients = new ConcurrentDictionary<int, Patient>();
            foreach (var patient in new List<Patient>
            {
                new Patient { Id = 1, FirstName = "John", LastName = "Doe", Age = 30, Gender = "Male", Diagnosis = "Hypertension" },                    
                new Patient { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 25, Gender = "Female", Diagnosis = "Diabetes" },
                new Patient { Id = 3, FirstName = "Bob", LastName = "Johnson", Age = 40, Gender = "Male", Diagnosis = "Asthma" }
            })
            {
                _patients.TryAdd(patient.Id, patient);
            }
        }

        public async Task<Patient?> GetPatient(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Key == id).Value;
            PiiProtectedLogger.LogInfo($"Retrieved patient with ID: {id}");
            return await Task.FromResult(patient);
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            if(_patients.Count == 0)
            {
                patient.Id = 1;
            }
            else            
            {
                patient.Id = _patients.Keys.Max() + 1;
            }
            
            var newPatient = new Patient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age,
                Gender = patient.Gender,
                Address = patient.Address,
                Diagnosis = patient.Diagnosis
            };
            _patients.TryAdd(newPatient.Id, newPatient);
            PiiProtectedLogger.LogInfo($"Created new patient with ID: {newPatient.Id}, Name: {newPatient.FirstName} {newPatient.LastName}");
            return await Task.FromResult(newPatient);
        }

        public async Task<Patient?> UpdatePatient(int id, Patient patient)
        {
            var oldPatient = _patients.FirstOrDefault(p => p.Key == id).Value;
            if (oldPatient == null)
            {
                PiiProtectedLogger.LogWarning($"Attempted to update non-existent patient with ID: {id}");
                return await Task.FromResult<Patient?>(null);
            }
            
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
            _patients.TryRemove(id, out _); 
            _patients.TryAdd(id, updatedPatient);
            PiiProtectedLogger.LogInfo($"Updated patient with ID: {id}");
            return await Task.FromResult(updatedPatient);
        }

        public async Task<bool> DeletePatient(int id)
        {
            var patient = _patients.FirstOrDefault(p => p.Key == id).Value;
            if (patient == null)
            {
                PiiProtectedLogger.LogWarning($"Attempted to delete non-existent patient with ID: {id}");
                return await Task.FromResult(false);
            }
            _patients.TryRemove(id, out _);
            PiiProtectedLogger.LogInfo($"Deleted patient with ID: {id}");
            return await Task.FromResult(true);
        }   
    }
}