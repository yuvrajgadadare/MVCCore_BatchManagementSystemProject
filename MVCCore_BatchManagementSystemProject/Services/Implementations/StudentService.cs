using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using System.Collections.Generic;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class StudentService : IStudentService
    {
        IRepository<TblstudentDetail> studentrepo;
        IRepository<TblstudentPayment> paymentrepo;
        IRepository<TblstudentRegistration> registrationrepo;
        IRepository<TbltrainingCourse> courserepo;
        IRepository<TbltrainingCourseFee> feerepo;
        IRepository<TbltrainingCourseTopic> coursetopicrepo;
        IRepository<TbltrainingTopic> topicrepo;
        IRepository<TbltopicContent> contentrepo;
        IExtraService extraService;

        public StudentService(IRepository<TblstudentDetail> studentrepo, IRepository<TblstudentPayment> paymentrepo, IRepository<TblstudentRegistration> registrationrepo, IRepository<TbltrainingCourse> courserepo, IRepository<TbltrainingCourseFee> feerepo, IRepository<TbltrainingCourseTopic> coursetopicrepo, IRepository<TbltrainingTopic> topicrepo, IRepository<TbltopicContent> contentrepo, IExtraService extraService)
        {
            this.studentrepo = studentrepo;
            this.paymentrepo = paymentrepo;
            this.registrationrepo = registrationrepo;
            this.courserepo = courserepo;
            this.feerepo = feerepo;
            this.coursetopicrepo = coursetopicrepo;
            this.topicrepo = topicrepo;
            this.contentrepo = contentrepo;
            this.extraService = extraService;
        }
        public void AddStudentDetails(TblstudentDetail detail)
        {
            studentrepo.Create(detail);
        }
        public void AddStudentPayment(TblstudentPayment payment)
        {
            paymentrepo.Create(payment);
        }
        public void AddStudentRegistration(TblstudentRegistration registration)
        {
            registrationrepo.Create(registration);
        }
        public TblstudentDetail CheckStudentLogin(string email_address, string password)
        {
            TblstudentDetail s = studentrepo.GetAll().FirstOrDefault(e => e.EmailAddress.Equals(email_address) && e.Password.Equals(password));
            return s;
        }
        public void DeleteStudentDetails(int detail_id)
        {
            studentrepo.Delete(detail_id);
        }
        public void DeleteStudentPayment(int payment_id)
        {
            paymentrepo.Delete(payment_id);
        }
        public void DeleteStudentRegistration(int registration_id)
        {
            registrationrepo.Delete(registration_id);
        }
        //public List<StudentPaymentModel> GetRegistrationWisePayments(int registration_id)
        //{
        //    throw new NotImplementedException();
        //}
        public List<TblstudentDetail> GetStudentDetails()
        {
            List<TblstudentDetail> lst = new List<TblstudentDetail>();
            foreach (TblstudentDetail s in studentrepo.GetAll())
            {
                List<TblstudentRegistration> registrations = new List<TblstudentRegistration>();
                foreach (TblstudentRegistration r in registrationrepo.GetAll().Where(e => e.StudentId.Equals(e.StudentId)).ToList())
                {
                    r.Fee = feerepo.GetById((int)r.FeeId);
                    List<TblstudentPayment> payments = paymentrepo.GetAll().Where(e => e.RegistrationId.Equals(r.RegistrationId)).ToList();
                    r.TblstudentPayments= payments;
                    registrations.Add(r);

                  
                }
                s.TblstudentRegistrations = registrations;
                lst.Add(s);
            }
            return lst;
        }
        public TblstudentRegistration GetStudentRegistrationDetails(int registration_id)
        {
            TblstudentRegistration r = registrationrepo.GetById(registration_id);
            TblstudentDetail sd = studentrepo.GetById((int)r.StudentId);
            r.Student = sd;
           TbltrainingCourseFee fee = feerepo.GetById((int)r.FeeId);
            TbltrainingCourse course = courserepo.GetById((int)fee.CourseId);
            fee.Course = course;
            r.Fee = fee;
            List<TblstudentPayment> payments = paymentrepo.GetAll().Where(e => e.RegistrationId.Equals(r.RegistrationId)).ToList();
            r.TblstudentPayments = payments;
            return r;
        }
        public List<StudentCourseModel> GetAllStudents()
        {

            List<StudentCourseModel> lst = new List<StudentCourseModel>();
            var query = from s in studentrepo.GetAll()
                        join r in registrationrepo.GetAll() on s.StudentId equals r.StudentId
                        join fr in feerepo.GetAll() on r.FeeId equals fr.FeeId
                        join cr in courserepo.GetAll() on fr.CourseId equals cr.CourseId
                        join ctp in coursetopicrepo.GetAll() on cr.CourseId equals ctp.CourseId

                        join t in topicrepo.GetAll() on ctp.TopicId equals t.TopicId

                        select new
                        {

                            topic_id = t.TopicId,
                            topic_name = t.TopicName,
                            student_id = s.StudentId,
                            student_name = s.StudentName,
                            registration_id = r.RegistrationId,
                            course_name = cr.CourseName,
                            course_id = cr.CourseId

                        };
            foreach (var q in query)
            {
                StudentCourseModel sm = new StudentCourseModel()
                {
                    RegistrationId = q.registration_id,
                    StudentName = q.student_name,
                    TopicId = q.topic_id,
                    TopicName = q.topic_name,
                    CourseId = q.course_id,
                    CourseName = q.course_name,
                    StudentId = q.student_id,

                };
                lst.Add(sm);
            }
            return lst;
        }
        public TblstudentDetail GetStudentDetails(int student_id)
        {
            TblstudentDetail stud = studentrepo.GetById(student_id);
            List<TblstudentDetail> students = studentrepo.GetAll();

            List<TblstudentRegistration> registrations = registrationrepo.GetAll().Where(e => e.StudentId.Equals(student_id)).ToList();
            List<TblstudentRegistration> registrationlist = new List<TblstudentRegistration>();
            foreach (TblstudentRegistration r in registrations)
            {

                TbltrainingCourseFee f = feerepo.GetById((int)r.FeeId);
                TbltrainingCourse course = courserepo.GetById((int)f.CourseId);

                List<TbltrainingCourseTopic> coursetopics = coursetopicrepo.GetAll().Where(e => e.CourseId.Equals(course.CourseId)).ToList();
                List<TbltrainingCourseTopic> topiclist = new List<TbltrainingCourseTopic>();

                foreach (TbltrainingCourseTopic t in coursetopics)
                {
                    TbltrainingTopic tp = topicrepo.GetById((int)t.TopicId);

                    List<TbltopicContent> contents = contentrepo.GetAll().Where(e => e.TopicId.Equals(tp.TopicId)).ToList();
                    tp.TbltopicContents = contents;
                    t.Topic = tp;
                    topiclist.Add(t);
                }
                course.TbltrainingCourseTopics = topiclist;
                f.Course = course;
                r.Fee = f;
                registrationlist.Add(r);
            }
            stud.TblstudentRegistrations = registrationlist;

            return stud;
        }
        public StudentCourseDetailsModel GetRegistartionWisePayments(int registration_id)
        {
            return GetStudentPayments().FirstOrDefault(e => e.RegistrationId.Equals(registration_id));
        }
        public List<StudentCourseDetailsModel> GetStudentPayments()
        {
            List<StudentCourseDetailsModel> lst = new List<StudentCourseDetailsModel>();
            foreach (TblstudentRegistration r in registrationrepo.GetAll())
            {
                TblstudentDetail s = studentrepo.GetById((int)r.StudentId);
                TbltrainingCourseFee f = feerepo.GetById((int)r.FeeId);
                TbltrainingCourse c = courserepo.GetById((int)f.CourseId);
                List<StudentPaymentModel> payments = new List<StudentPaymentModel>();
                float total_fees_amount = 0, paid_amount = 0, remaining_amount = 0;
                total_fees_amount = (float)((f.FeesAmount + (f.FeesAmount * f.Gst / 100)) - r.Discount);
                foreach (TblstudentPayment p in paymentrepo.GetAll().Where(e => e.RegistrationId.Equals(r.RegistrationId)).ToList())
                {
                    paid_amount += (float)p.PaymentAmount;
                    StudentPaymentModel pm = new StudentPaymentModel()
                    {
                        RegistrationId = (int)p.RegistrationId,
                        PaymentAmount = p.PaymentAmount,
                        PaymentDate = p.PaymentDate,
                        PaymentDescription = p.PaymentDescription,
                        PaymentId = p.PaymentId,
                        PaymentMode = p.PaymentMode
                    };
                    payments.Add(pm);
                }
                remaining_amount = total_fees_amount - paid_amount;
                StudentCourseDetailsModel sd = new StudentCourseDetailsModel()
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    FeeId = f.FeeId,
                    BirthDate = s.BirthDate,
                    EmailAddress = s.EmailAddress,
                    Gender = s.Gender,
                    MobileNumber = s.MobileNumber,
                    ProfilePhoto = s.ProfilePhoto,
                    Qualification = s.Qualification,
                    Discount = (float)r.Discount,
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    FeeAmount = (float)f.FeesAmount,
                    FeeMode = f.FeeMode,
                    Gst = (float)f.Gst,
                    RegistrationDate = (DateTime)r.RegistrationDate,
                    RegistrationId = r.RegistrationId,
                    total_fees_amount = total_fees_amount,
                    paid_amount = paid_amount,
                    remaining_amount = remaining_amount,
                    payments = payments,
                };
                lst.Add(sd);
            }
            return lst;
        }
        public StudentRegistrationModel GetStudentRegistration(int registration_id)
        {
            return GetStudentRegistrations().FirstOrDefault(e => e.RegistrationId.Equals(registration_id));
        }

        public List<StudentRegistrationModel> GetStudentRegistrations()
        {
            List<StudentRegistrationModel> lst = new List<StudentRegistrationModel>();
            var query = from s in studentrepo.GetAll()
                        join r in registrationrepo.GetAll()
                      on s.StudentId equals r.StudentId
                        join crf in feerepo.GetAll() on r.FeeId equals crf.FeeId
                        join cr in courserepo.GetAll() on crf.CourseId equals cr.CourseId
                        select new
                        {
                            student_id = s.StudentId,
                            student_name = s.StudentName,
                            gender = s.Gender,
                            email_address = s.EmailAddress,
                            mobile_number = s.MobileNumber,
                            birth_date = s.BirthDate,
                            profile_photo = s.ProfilePhoto,
                            qualification = s.Qualification,
                            registration_id = r.RegistrationId,
                            registration_date = r.RegistrationDate,
                            course_id = cr.CourseId,
                            course_name = cr.CourseName,
                            discount = r.Discount,
                            fee_id = crf.FeeId,
                            fee_mode = crf.FeeMode,
                            total_fees = crf.FeesAmount,
                            gst = crf.Gst
                        };
            foreach (var q in query)
            {
                StudentRegistrationModel rm = new StudentRegistrationModel()
                {
                    CourseId = (int)q.course_id,
                    CourseName = q.course_name,
                    Gender = q.gender,
                    Discount = q.discount,
                    FeeId = q.fee_id,
                    FeesAmount = q.total_fees,
                    FeeMode = q.fee_mode,
                    Gst = q.gst,
                    BirthDate = q.birth_date,
                    EmailAddress = q.email_address,
                    MobileNumber = q.mobile_number,
                    ProfilePhoto = q.profile_photo,
                    RegistrationDate = q.registration_date,
                    RegistrationId = q.registration_id,
                    Qualification = q.qualification,
                    StudentId = q.student_id,
                    StudentName = q.student_name
                };
                lst.Add(rm);
            }
            return lst;
        }

        public void UpdateStudentDetails(TblstudentDetail detail)
        {
            studentrepo.Update(detail);
        }

        public void UpdateStudentPayment(TblstudentPayment payment)
        {
            paymentrepo.Update(payment);
        }

        public StudentPaymentModel GetStudentPayment(int payment_id)
        {
            TblstudentPayment p = paymentrepo.GetById(payment_id);
            StudentCourseDetailsModel cm = GetRegistartionWisePayments((int)p.RegistrationId);
            List<TblstudentPayment> payments = paymentrepo.GetAll().Where(e => e.RegistrationId.Equals(p.RegistrationId) && e.PaymentId < payment_id).ToList();
            float paid_amount = 0, remaining_amount = 0;
            foreach (TblstudentPayment pm in payments)
            {
                paid_amount += (float)pm.PaymentAmount;
            }
            remaining_amount = cm.total_fees_amount - paid_amount;
            StudentPaymentModel sp = new StudentPaymentModel() {
                PaymentId = p.PaymentId,
                PaymentAmount = p.PaymentAmount,
                PaymentDate = p.PaymentDate,
                RegistrationId = (int)p.RegistrationId,
                PaymentDescription = p.PaymentDescription,
                PaymentMode = p.PaymentMode,
                TotalFees = cm.total_fees_amount,
                PaidTillDate = paid_amount,
                RemainingAmountTillDate = remaining_amount,
                fees_in_word = extraService.ConvertAmount(cm.total_fees_amount)
            };
            return sp;
        }
        public List<StudentPaymentModel> GetRegistrationWisePayments(int payment_id)
        {
            TblstudentPayment p = paymentrepo.GetById(payment_id);
            List<StudentPaymentModel> lst = new List<StudentPaymentModel>();
            List<TblstudentPayment> payments = paymentrepo.GetAll().Where(e => e.RegistrationId.Equals(p.RegistrationId) && e.PaymentId < payment_id).ToList();
            float paid_amount = 0, remaining_amount = 0;
            foreach (TblstudentPayment pm in payments)
            {

                StudentPaymentModel sp = new StudentPaymentModel()
                {
                    PaymentId = pm.PaymentId,
                    PaymentAmount = pm.PaymentAmount,
                    PaymentDate = pm.PaymentDate,
                    RegistrationId = (int)pm.RegistrationId,
                    PaymentDescription = pm.PaymentDescription,
                    PaymentMode = pm.PaymentMode,

                };
                lst.Add(sp);
            }
            return lst;
        }

        public List<StudentPaymentModel> GetAllPayments()
        {

            List<StudentPaymentModel> lst = new List<StudentPaymentModel>();
            List<TblstudentPayment> payments = paymentrepo.GetAll();
            float paid_amount = 0, remaining_amount = 0;

            foreach (TblstudentPayment pm in payments)
            {
                TblstudentRegistration r = registrationrepo.GetById((int)pm.RegistrationId);
                TblstudentDetail sd = studentrepo.GetById((int)r.StudentId);

                StudentPaymentModel sp = new StudentPaymentModel()
                {
                    PaymentId = pm.PaymentId,
                    PaymentAmount = pm.PaymentAmount,
                    PaymentDate = pm.PaymentDate,
                    RegistrationId = (int)pm.RegistrationId,
                    PaymentDescription = pm.PaymentDescription,
                    PaymentMode = pm.PaymentMode,
                    student_name = sd.StudentName

                };
                lst.Add(sp);
            }
            return lst;
        }
        public void UpdateStudentRegistration(TblstudentRegistration registration)
        {
            registrationrepo.Update(registration);
        }

        
    }
}
 