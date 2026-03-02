using Microsoft.AspNetCore.Mvc;
using MyWebApi.Model;   
using MyWebApi.Interfaces;
namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("DupageCommunityHospital")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILoggerService _logger;    
        public PatientController(IPatientService patientService, ILoggerService logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

         [HttpGet("patients")]
        public async Task<IActionResult> GetPatients()
        {
            _logger.LogInfo($"Attempting to retrieve all patients");
            var result = await _patientService.GetPatients();
            _logger.LogInfo($"Successfully retrieved all patients");
            return Ok(result);
        }

        [HttpGet("patients/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            _logger.LogInfo($"Attempting to retrieve patient with ID: {id}");
            var result = await _patientService.GetPatient(id);
            _logger.LogInfo($"Successfully retrieved patient with ID: {id}");
            return Ok(result);
        }

        [HttpPost("patients/register")]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {
            _logger.LogInfo($"Attempting to create a new patient with name: {patient.FirstName} {patient.LastName}  and age: {patient.Age}");
            var result = await _patientService.CreatePatient(patient);
            _logger.LogInfo($"Successfully created a new patient with name: {patient.FirstName} {patient.LastName}  and age: {patient.Age}");
            return Ok(result);
        }

        [HttpPut("patients/{id}/update")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            _logger.LogInfo($"Attempting to update patient with ID: {id}");
            var result = await _patientService.UpdatePatient(id, patient);
            _logger.LogInfo($"Successfully updated patient with ID: {id}");
            return Ok(result);  
        }

        [HttpDelete("patients/{id}/delete")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            _logger.LogInfo($"Attempting to delete patient with ID: {id}");
            var result = await _patientService.DeletePatient(id);
            _logger.LogInfo($"Successfully deleted patient with ID: {id}");
            return Ok(result);
        }   
    }
}   
