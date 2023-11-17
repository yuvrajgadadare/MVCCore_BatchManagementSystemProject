namespace MVCCore_BatchManagementSystemProject.Models
{
    public class BatchAttendanceModel
    {
        public int batch_id { get; set; }
        public int content_id { get; set; }
        public string content_name { get; set; }
        public DateTime expected_date { get; set; }
        public DateTime actual_date { get; set; }
        public List<AttendanceStudentModel> students { get; set; }
    }
}
