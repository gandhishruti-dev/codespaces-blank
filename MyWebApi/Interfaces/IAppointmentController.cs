using MyWebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Interfaces
{
    public interface IAppointmentController
    {
        // Define methods for appointment management, e.g.:
        Task<IActionResult> GetPatientAppointments(int patientId);
        Task<IActionResult> GetAppointmentDetails(int appointmentId);
        Task<IActionResult> ScheduleAppointment(PatientAppointment appointment);
        Task<IActionResult> UpdateAppointment(int appointmentId, PatientAppointment appointment);
        Task<IActionResult> CancelAppointment(int appointmentId);
        Task<IActionResult> DeleteAppointmentByPatientId(int id, int appointmentId);
        Task<IActionResult> DeleteAppointment(int id);
        Task<IActionResult> GetAppointmentLocation(int appointmentId);
    }

    public interface IAppointmentService
    {
        // Define methods for appointment management, e.g.:
        Task<IActionResult> GetPatientAppointments(int patientId);
        Task<IActionResult> GetAppointmentDetails(int appointmentId);
        Task<IActionResult> ScheduleAppointment(PatientAppointment appointment);
        Task<IActionResult> UpdateAppointment(int appointmentId, PatientAppointment appointment);
        Task<IActionResult> CancelAppointment(int appointmentId);
        Task<IActionResult> DeleteAppointmentByPatientId(int id, int appointmentId);
        Task<IActionResult> DeleteAppointment(int id);
        Task<IActionResult> GetAppointmentLocation(int appointmentId);
    }
}       