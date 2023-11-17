namespace MVCCore_BatchManagementSystemProject.Models
{
    public class BatchStudentAttendanceModel
    {
        public int student_id {  get; set; }
        public int registration_id {  get; set; }
        public string student_name {  get; set; }
        public int attendance_id { get; set; }
        public int topic_id { get; set; }
        public int content_id { get; set; }
        public string content_name { get; set; }
        public string topic_name { get; set; }
        public int is_present { get; set; }
        public string expected_date { get; set; }
        public string actual_date { get; set; }

        public string status { get; set; }
        public string color { get; set; }
        public string delay_or_advance {  get; set; }
    }
}
