using MVCCore_BatchManagementSystemProject.Models;

namespace MVCCore_BatchManagementSystemProject.Services.Interfaces
{
    public interface IStudentService
    {
        void AddStudentDetails(TblstudentDetail detail);
        void UpdateStudentDetails(TblstudentDetail detail);
        void DeleteStudentDetails(int detail_id);
        List<TblstudentDetail> GetStudentDetails();
        List<StudentCourseModel> GetAllStudents();

        TblstudentDetail GetStudentDetails(int student_id);
        TblstudentDetail CheckStudentLogin(string email_address,string password);
        void AddStudentRegistration(TblstudentRegistration registration);
        void UpdateStudentRegistration(TblstudentRegistration registration);
        void DeleteStudentRegistration(int registration_id);
        List<StudentRegistrationModel> GetStudentRegistrations();
        StudentRegistrationModel GetStudentRegistration(int registration_id);
        void AddStudentPayment(TblstudentPayment payment);
        void UpdateStudentPayment(TblstudentPayment payment);
        void DeleteStudentPayment(int payment_id);
        //List<StudentPaymentModel> GetStudentPayments();
        StudentCourseDetailsModel GetRegistartionWisePayments(int registartion_id);
        List<StudentCourseDetailsModel> GetStudentPayments();
        StudentPaymentModel GetStudentPayment(int payment_id);
        List<StudentPaymentModel> GetRegistrationWisePayments(int payment_id);
        //List<StudentPaymentModel> GetRegistrationWisePayments(int registration_id);
        TblstudentRegistration GetStudentRegistrationDetails(int registration_id);
        List<StudentPaymentModel> GetAllPayments();
    }
}
