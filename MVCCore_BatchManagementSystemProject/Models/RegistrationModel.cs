namespace MVCCore_BatchManagementSystemProject.Models
{
    public class RegistrationModel
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }

        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Qualification { get; set; }

        public string? Address { get; set; }

        public string EmailAddress { get; set; } = null!;
        public string? MobileNumber { get; set; }
        public string? ProfilePhoto { get; set; }
        public DateTime? RegistrationDate { get; set; }
     public int FeeId { get; set; }
     public float Gst { get; set; }
     public float Fees { get; set; }
        public float Discount { get; set; }
        public float FinalTrainingFees { get; set; }
        public float RegistrationAmount { get; set; }

        public string? Password { get; set; }




    }
}
