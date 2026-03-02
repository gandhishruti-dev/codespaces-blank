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
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("patients/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            return Ok(await _patientService.GetPatient(id));
        }

        [HttpPost("patients/register")]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {
            return Ok(await _patientService.CreatePatient(patient));
        }

        [HttpPut("patients/{id}/update")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            return Ok(await _patientService.UpdatePatient(id, patient));
        }

        [HttpDelete("patients/{id}/delete")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            return Ok(await _patientService.DeletePatient(id));
        }   
    }
}   
