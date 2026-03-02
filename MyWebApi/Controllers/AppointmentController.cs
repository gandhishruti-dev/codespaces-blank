using Microsoft.AspNetCore.Mvc;
using MyWebApi.Model;
using MyWebApi.Interfaces;


namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("DupageCommunityHospital")]
    public class AppointmentController : ControllerBase, IAppointmentController
    {
        [HttpGet("/patients/{id}/appointments")]
        public async Task<IActionResult> GetPatientAppointments(int id)
        {
            var appointments = new List<PatientAppointment>
            {
                new PatientAppointment 
                {
                    AppointmentId = 1,
                    PatientId = id,
                    AppointmentDate = DateTime.Now.AddDays(7),
                    DoctorName = "Dr. Smith",
                    Reason = "Routine Checkup",
                    AppointmentLocation = new Address
                    {
                        Id = 2,
                        AddressLine1 = "456 Clinic Road",
                        City = "Anytown",
                        State = "CA",
                        ZipCode = "12345"
                    }
                },
                new PatientAppointment 
                {
                    AppointmentId = 2,
                    PatientId = id,
                    AppointmentDate = DateTime.Now.AddDays(30),
                    DoctorName = "Dr. Lee",
                    Reason = "Follow-up",
                    AppointmentLocation = new Address
                    {
                        Id = 3,
                        AddressLine1 = "789 Hospital Ave",
                        City = "Anytown",
                        State = "CA",
                        ZipCode = "12345"
                    }
                }
            };
            return Ok(appointments);
        }

        [HttpGet("/appointments/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentDetails(int appointmentId)
        {
            var appointment = new PatientAppointment
            {
                AppointmentId = appointmentId,
                PatientId = 1,
                AppointmentDate = DateTime.Now.AddDays(7),
                DoctorName = "Dr. Smith",
                Reason = "Routine Checkup",
                AppointmentLocation = new Address
                {
                    Id = 2,
                    AddressLine1 = "456 Clinic Road",
                    City = "Anytown",
                    State = "CA",
                    ZipCode = "12345"
                }
            };
            return Ok(appointment);
        }

        [HttpPost("/appointments/schedule")]
        public async Task<IActionResult> ScheduleAppointment(PatientAppointment appointment)
        {
            return Ok(appointment);
        }

        [HttpDelete("/appointments/{appointmentId}/cancel")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {            
            return Ok(new { Message = $"Appointment with ID {appointmentId} cancelled successfully." });
        }        

         [HttpDelete("/appointments/{id}/delete")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            return Ok(new { Message = $"Appointment with ID {id} deleted successfully." });
        }
        
        [HttpDelete("/patients/{id}/appointments/{appointmentId}/delete")]
        public async Task<IActionResult> DeleteAppointmentByPatientId(int id, int appointmentId)
        {
            return Ok(new { Message = $"Appointment with ID {appointmentId} for patient with ID {id} deleted successfully." });
        }

        [HttpPut("/appointments/{appointmentId}/update")]
        public async Task<IActionResult> UpdateAppointment(int appointmentId, PatientAppointment appointment)
        {
            return Ok(appointment);
        }

        [HttpGet("/appointments/{appointmentId}/location")]
        public async Task<IActionResult> GetAppointmentLocation(int appointmentId)
        {
            var location = new Address
            {
                Id = 2,
                AddressLine1 = "456 Clinic Road",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };
            return Ok(location);
        }
    }
}