namespace MyWebApi.Model
{
    public class PatientAppointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public required string DoctorName { get; set; }
        public required string Reason { get; set; }
        public Address? AppointmentLocation { get; set; }
    }
}