namespace MyWebApi.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
        public Address? Address { get; set; }
        public string? Diagnosis { get; set; }
    }
}