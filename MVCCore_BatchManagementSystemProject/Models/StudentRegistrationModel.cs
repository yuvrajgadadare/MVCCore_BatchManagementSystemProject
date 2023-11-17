namespace MVCCore_BatchManagementSystemProject.Models
{
    public class StudentRegistrationModel
    {
        public int StudentId { get; set; }

        public string? StudentName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Qualification { get; set; }
        public int RegistrationId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ProfilePhoto { get; set; }

        public double? Discount { get; set; }
        public int FeeId { get; set; }

        public double FeesAmount { get; set; }

        public double? Gst { get; set; }

        public string? FeeMode { get; set; }

    }
}
