using MVCCore_BatchManagementSystemProject.Models;
namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface IBatchService
    {
        void AddBatch(Tblbatch batch);
        void UpdateBatch(Tblbatch batch);
        void DeleteBatch(int batch_id);
        List<BatchModel> GetBatches();
        Tblbatch GetBatch(int batch_id);
        void AddBatchStudent(TblbatchStudent batch_student);
        void UpdateBatchStudent(TblbatchStudent batch_Student);
        void DeleteBatchStudent(int batch_student_id);
        void AddBatchSchedule(TblbatchSchedule b);
        void UpdateBatchSchedule(TblbatchSchedule b);
        void AddAttendance(TblbatchScheduleAttendance s);
        List<StudentBatchModel> GetBatchStudents();
        StudentBatchModel GetBatchStudent(int batch_student_id);
        List<StudentBatchModel> GetBatchWiseStudents(int batch_id);
        List<Tblbatch> GetTrainerWiseBatches(int trainer_id);
        List<StudentBatchModel> GetStudentWiseBatches(int student_id);

        List<TblbatchSchedule> GetBatchWiseSchedule(int batch_id);
        List<Tblbatch> GetAllBatches();
        List<TblbatchSchedule> GetBatchSchedule(int batch_id);

        TblbatchSchedule GetScheduleById(int schedule_id);

        List<BatchAttendanceModel> GetBatchWiseAttendance(int batch_id);
        List<BatchStudentAttendanceModel> GetStudentWiseBatchAttendance(int batch_id, int registration_id);
       // List<BatchStudentAttendanceModel> GetStudentWiseBatchDetails(int registration_id);
        List<Tblbatch> GetStudentWiseBatchdetails(int registration_id);
    }
}
