using Microsoft.Identity.Client;

namespace MVCCore_BatchManagementSystemProject.Models
{
    public class StudentCourseDetailsModel
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Gender { get; set; }

        public string? MobileNumber { get; set; }

        public string EmailAddress { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public string? ProfilePhoto { get; set; }

        public string? Qualification { get; set; }

        public int RegistrationId { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int FeeId { get; set; }
        public string FeeMode { get; set; }
        public float FeeAmount { get; set; }
        public float Discount { get; set; }
        public float Gst { get; set; }
        public float paid_amount {  get; set; }
        public float total_fees_amount {  get; set; }
        public float remaining_amount {  get; set; }

        public List<StudentPaymentModel> payments;


    }
}
