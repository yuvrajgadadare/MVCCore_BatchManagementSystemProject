namespace MVCCore_BatchManagementSystemProject.Models
{
    public class StudentPaymentModel
    {
        public int PaymentId { get; set; }
        public int RegistrationId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double? PaymentAmount { get; set; }
        public string? PaymentMode { get; set; }
        public string? PaymentDescription { get; set; }
        public float PaidTillDate { get; set; }
        public float RemainingAmountTillDate { get; set; }
        public float TotalFees { get; set; }
        public string fees_in_word { get; set; }
        public string student_name {  get; set; }
    }
}