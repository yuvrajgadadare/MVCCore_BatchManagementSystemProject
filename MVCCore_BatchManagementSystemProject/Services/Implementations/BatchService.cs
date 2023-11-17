using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using System.Collections.Generic;
using System.Drawing;

namespace MVCCore_BatchManagementSystemProject.Services.Implementations
{
    public class BatchService : IBatchService
    {
        IRepository<Tblbatch> batchrepo;
        IRepository<TblbatchStudent> batchstudentrepo;
        IRepository<TblstudentDetail> studentrepo;
        IRepository<TblstudentRegistration> registrationrepo;
        IRepository<TbltrainingTopic> topicrepo;
        IRepository<Tbltrainer> trainerrepo;
        IRepository<TbltrainingCourseFee> feerepo;
        IRepository<TbltrainingCourse> courserepo;
        IRepository<TbltrainingCourseTopic> coursetopicrepo;
        IRepository<TblbatchSchedule> schedulerepo;
        IRepository<TbltopicContent> contentrepo;
        IRepository<Tblholiday> holidayrepo;
       IRepository<TblbatchScheduleAttendance> attendancerepo;
        IRepository<TblscheduleAttendance> scheduleattenancerepo;
        public BatchService(IRepository<Tblbatch> batchrepo, IRepository<TblbatchStudent> batchstudentrepo, IRepository<TblstudentDetail> studentrepo, IRepository<TblstudentRegistration> registrationrepo, IRepository<TbltrainingTopic> topicrepo,IRepository<Tbltrainer> trainerrepo, IRepository<TbltrainingCourseFee> feerepo, IRepository<TbltrainingCourse> courserepo, IRepository<TblbatchSchedule> schedulerepo,IRepository<TbltopicContent> contentrepo, IRepository<Tblholiday> holidayrepo, IRepository<TblbatchScheduleAttendance> attendancerepo, IRepository<TblscheduleAttendance> scheduleattenancerepo)
        {
            this.batchrepo = batchrepo;
            this.batchstudentrepo = batchstudentrepo;
            this.studentrepo = studentrepo;
            this.registrationrepo = registrationrepo;
            this.topicrepo = topicrepo;
            this.trainerrepo = trainerrepo;
            this.courserepo = courserepo;
            this.feerepo = feerepo;
            this.schedulerepo = schedulerepo;
            this.contentrepo = contentrepo;
            this.holidayrepo = holidayrepo;
            this.attendancerepo= attendancerepo;
            this.scheduleattenancerepo = scheduleattenancerepo;
        }

        public void AddBatch(Tblbatch batch)
        {

            batchrepo.Create(batch);
        }

        public void AddBatchStudent(TblbatchStudent batch_student)
        {

            batchstudentrepo.Create(batch_student);
        }

        public void DeleteBatch(int batch_id)
        {

            batchrepo.Delete(batch_id);

        }

        public void DeleteBatchStudent(int batch_student_id)
        {
            batchstudentrepo.Delete(batch_student_id);
        }

        public Tblbatch GetBatch(int batch_id)
        {

            return batchrepo.GetById(batch_id);
        }

        public List<BatchModel> GetBatches()
        {
            List<BatchModel> lst = new List<BatchModel>();
            foreach (Tblbatch b in batchrepo.GetAll())
            {
                List<TblbatchSchedule> schedules = schedulerepo.GetAll().Where(e => e.BatchId.Equals(b.BatchId)).ToList();

                BatchModel sm = new BatchModel()
                {
                    BatchId = b.BatchId,
                    BatchName = b.BatchName,
                    BatchTime = b.BatchTime,
                    EndDate = b.EndDate,
                    StartDate = b.StartDate,
                    Status = b.Status,
                    TopicId = b.TopicId,
                    TrainerId = b.TrainerId,
                    TopicName = topicrepo.GetById((int)b.TopicId).TopicName,
                    TrainerName = trainerrepo.GetById((int)b.TrainerId).TrainerName,
                     TotalStudents= batchstudentrepo.GetAll().Where(e=>e.BatchId.Equals(b.BatchId)).ToList().Count(),
                     total_schedule_days= schedules.Count()
                };
                lst.Add(sm);
            }
            return lst;

        }

        public List<Tblbatch> GetTrainerWiseBatches(int trainer_id)
        {
            List<Tblbatch> lst = GetAllBatches().Where(e=>e.TrainerId.Equals(trainer_id)).ToList();
            return lst;
        }
        public List<Tblbatch> GetAllBatches()
        {
            List<Tblbatch> lst = batchrepo.GetAll();
            List<Tblbatch> batchlist = new List<Tblbatch>();
            foreach (Tblbatch b in lst)
            {
                Tbltrainer trainer = trainerrepo.GetById((int)b.TrainerId);
                b.Trainer = trainer;
                TbltrainingTopic topic = topicrepo.GetById((int)b.TopicId);
                b.Topic = topic;
                List<TblbatchStudent> batchstudents = batchstudentrepo.GetAll().Where(e => e.BatchId.Equals(b.BatchId)).ToList();
                List<TblbatchStudent> batchstudentslist = new List<TblbatchStudent>();
                foreach (TblbatchStudent s in batchstudents)
                {
                    TblstudentRegistration rst = registrationrepo.GetById((int)s.RegistrationId);
                    TbltrainingCourseFee fee = feerepo.GetById((int)rst.FeeId);
                    TbltrainingCourse course = courserepo.GetById((int)fee.CourseId);
                    fee.Course = course;
                    TblstudentDetail st = studentrepo.GetById((int)rst.StudentId);

                    rst.Fee = fee;


                    s.Registration = rst;

                    rst.Student = st;
                    s.Registration = rst;
                    batchstudentslist.Add(s);
                }
                b.TblbatchStudents = batchstudentslist;
                batchlist.Add(b);

            }

            return batchlist;

        }
        public StudentBatchModel GetBatchStudent(int batch_student_id)
        {
             return   GetBatchStudents().FirstOrDefault(e => e.StudentBatchId.Equals(batch_student_id));
        }

        public List<StudentBatchModel> GetBatchStudents()
        {
            List<StudentBatchModel> lst = new List<StudentBatchModel>();
            var query = from s in studentrepo.GetAll()
                        join r in registrationrepo.GetAll() on s.StudentId equals r.StudentId
                        join fr in feerepo.GetAll()  on r.FeeId equals fr.FeeId
                        join cr in courserepo.GetAll() on fr.CourseId equals cr.CourseId
                        join
                      sb in batchstudentrepo.GetAll() on r.RegistrationId equals sb.RegistrationId
                        join
                      b in batchrepo.GetAll() on sb.BatchId equals b.BatchId
                      join tr in trainerrepo.GetAll() on b.TrainerId equals tr.TrainerId
                      join t in topicrepo.GetAll() on b.TopicId equals t.TopicId
                     
                        select new {
                         batch_id=b.BatchId,
                         batch_name=b.BatchName,
                         start_date=b.StartDate,
                         end_date=b.EndDate,
                          batch_time=b.BatchTime,
                           topic_id=t.TopicId,
                           topic_name=t.TopicName,
                           student_id=s.StudentId,
                            student_name=s.StudentName,
                             registration_id=r.RegistrationId,
                              trainer_id=tr.TrainerId,
                               trainer_name=tr.TrainerName,
                               status=b.Status,
                               student_batch_id=sb.BatchStudentId,
                               course_name=cr.CourseName,
                               course_id=cr.CourseId
                        };
            foreach(var q in query)
            {
                StudentBatchModel sm = new StudentBatchModel() 
                {
                 RegistrationId=q.registration_id,
                  BatchId=q.batch_id,
                   BatchName=q.batch_name,
                    BatchTime=q.batch_time,
                     EndDate=q.end_date,
                      StartDate=q.start_date,
                       StudentId=q.student_id,
                        StudentName=q.student_name,
                         TopicId=q.topic_id,
                          TopicName=q.topic_name,
                           TrainerId=q.trainer_id,
                            TrainerName=q.trainer_name,
                            Status=q.status,
                             StudentBatchId=q.student_batch_id,
                              CourseId=q.course_id,
                               CourseName=q.course_name

                };
                lst.Add(sm);
            }
            return lst;
        }

        //public List<StudentBatchModel> GetBatchWiseStudents(int batch_id)
        //{
        //    return GetBatchStudents().Where(e => e.BatchId.Equals(batch_id)).ToList();
        //}

        public List<StudentBatchModel> GetStudentWiseBatches(int student_id)
        {
            return GetBatchStudents().Where(e => e.StudentId.Equals(student_id)).ToList();
        }
        public void UpdateBatch(Tblbatch batch)
        {
            batchrepo.Update(batch);
        }
        public void UpdateBatchStudent(TblbatchStudent batch_Student)
        {
            batchstudentrepo.Update(batch_Student);
        }
        public List<TblbatchSchedule> GetBatchWiseSchedule(int batch_id)
        {
            // Tblbatch b = GetAllBatches().FirstOrDefault(e => e.BatchId.Equals(batch_id));
            Tblbatch b = batchrepo.GetById(batch_id);
            DateTime sdt = (DateTime)b.StartDate;
            List<Tblholiday> holidays = holidayrepo.GetAll();
            TbltrainingTopic topic = topicrepo.GetById((int)b.TopicId);
            List<TbltopicContent> contents = contentrepo.GetAll().Where(e => e.TopicId.Equals(topic.TopicId)).ToList();
            int count = contents.Count;
            List<TblbatchSchedule> lst = schedulerepo.GetAll().Where(e => e.BatchId.Equals(batch_id)).ToList();
            List<TblbatchSchedule> schedulelist = new List<TblbatchSchedule>();
            if (lst.Count == 0)
            {
                foreach (var c in contents)
                {
                    Tblholiday hd = holidays.FirstOrDefault(e => e.HolidayDate.Equals(sdt));
                    if (hd != null)
                    {
                        sdt = sdt.AddDays(1);
                    }
                    if (sdt.DayOfWeek.ToString() == "Sunday")
                    {
                        sdt = sdt.AddDays(1);
                    }
                    if (sdt.DayOfWeek.ToString() == "Saturday")
                    {
                        sdt = sdt.AddDays(2);
                    }
                    TblbatchSchedule bt = new TblbatchSchedule()
                    {
                        BatchId = b.BatchId,
                        ContentId = c.ContentId,
                        ExpectedDate = sdt,
                        Content = c,
                        //BatchScheduleId=b.BatchId ,

                    };
                    sdt = sdt.AddDays(1);
                    schedulelist.Add(bt);
                }

            }
            else
            {
                schedulelist = lst;
            }
                return schedulelist;
        }
        //public void AddAttendance(TblbatchScheduleAttendance s)
        //{
        //    TblbatchScheduleAttendance at = new TblbatchScheduleAttendance() {
        //     AttendanceDate=s.AttendanceDate,
        //      BatchScheduleId = s.BatchScheduleId,
        //       TblscheduleAttendances=s.TblscheduleAttendances
        //    };


        //    TblbatchSchedule sch = schedulerepo.GetById((int)s.BatchScheduleId);
        //    sch.ActualDate = s.AttendanceDate;
        //   // schedulerepo.Update(sch);
        //    attendancerepo.Create(at);
        //}

        public void AddAttendance(TblbatchScheduleAttendance s)
        {
            
          
            attendancerepo.Create(s);


            TblbatchSchedule sd = GetScheduleById((int)s.BatchScheduleId);
            sd.ActualDate =s.AttendanceDate  ;
            UpdateBatchSchedule(sd);


        }
        public void UpdateBatchSchedule(TblbatchSchedule s)
        {
            schedulerepo.Update(s);
        }
        public List<StudentBatchModel> GetBatchWiseStudents(int batch_id)
        {
            List<StudentBatchModel> lst = new List<StudentBatchModel>();
            var query = from s in studentrepo.GetAll()
                        join r in registrationrepo.GetAll() on s.StudentId equals r.StudentId
                        join fr in feerepo.GetAll() on r.FeeId equals fr.FeeId
                        join cr in courserepo.GetAll() on fr.CourseId equals cr.CourseId
                        join
                      sb in batchstudentrepo.GetAll() on r.RegistrationId equals sb.RegistrationId
                        join
                      b in batchrepo.GetAll() on sb.BatchId equals b.BatchId
                        join tr in trainerrepo.GetAll() on b.TrainerId equals tr.TrainerId
                         where b.BatchId.Equals(batch_id)

                        select new
                        {
                            batch_id = b.BatchId,
                            batch_name = b.BatchName,
                            start_date = b.StartDate,
                            end_date = b.EndDate,
                            batch_time = b.BatchTime,

                            student_id = s.StudentId,
                            student_name = s.StudentName,
                            registration_id = r.RegistrationId,
                            trainer_id = tr.TrainerId,
                            trainer_name = tr.TrainerName,
                            status = b.Status,
                            student_batch_id = sb.BatchStudentId,
                            course_name = cr.CourseName,
                            course_id = cr.CourseId
                        };
            foreach (var q in query)
            {
                StudentBatchModel sm = new StudentBatchModel()
                {
                    RegistrationId = q.registration_id,
                    BatchId = q.batch_id,
                    BatchName = q.batch_name,
                    BatchTime = q.batch_time,
                    EndDate = q.end_date,
                    StartDate = q.start_date,
                    StudentId = q.student_id,
                    StudentName = q.student_name,
                    
                    TrainerId = q.trainer_id,
                    TrainerName = q.trainer_name,
                    Status = q.status,
                    StudentBatchId = q.student_batch_id,
                    CourseId = q.course_id,
                    CourseName = q.course_name

                };
                lst.Add(sm);
            }
            return lst;
        }

        public void AddBatchSchedule(TblbatchSchedule b)
        {
            schedulerepo.Create(b);

        }

        public List<TblbatchSchedule>GetBatchSchedule(int batch_id)
        {
            List<TblbatchSchedule> schedule = schedulerepo.GetAll().Where(e => e.BatchId.Equals(batch_id)).ToList();
            List<TblbatchSchedule> lst = new List<TblbatchSchedule>();
            foreach(TblbatchSchedule s in schedule)
            {
                TbltopicContent content = contentrepo.GetById((int)s.ContentId);
                content.TblbatchSchedules.Clear();
                content.Topic = null;
              
                TblbatchSchedule sc = new TblbatchSchedule() 
                {
                  BatchId=s.BatchId,
                   ContentId=s.ContentId,
                    ActualDate=s.ActualDate,
                     ExpectedDate=s.ExpectedDate,
                      BatchScheduleId=s.BatchScheduleId,
                      Content=content
                       
                };
                lst.Add(sc);
            }
            return lst;
        }
        public List<TblbatchSchedule> GetAllBatchesSchedule()
        {
            List<TblbatchSchedule> schedule = schedulerepo.GetAll();
            List<TblbatchSchedule> lst = new List<TblbatchSchedule>();
            foreach (TblbatchSchedule s in schedule)
            {
                TbltopicContent content = contentrepo.GetById((int)s.ContentId);
                content.TblbatchSchedules.Clear();
                content.Topic = null;

                TblbatchSchedule sc = new TblbatchSchedule()
                {
                    BatchId = s.BatchId,
                    ContentId = s.ContentId,
                    ActualDate = s.ActualDate,
                    ExpectedDate = s.ExpectedDate,
                    BatchScheduleId = s.BatchScheduleId,
                    Content = content

                };
                lst.Add(sc);
            }
            return lst;
        }
        public TblbatchSchedule GetScheduleById(int schedule_id)
        {
            TblbatchSchedule sd=schedulerepo.GetById(schedule_id);
            return sd;
        }

        public List<BatchAttendanceModel>GetBatchWiseAttendance(int batch_id)
        {
            List<BatchAttendanceModel> lst = new List<BatchAttendanceModel>();

            List<TblbatchSchedule> schedule = GetBatchSchedule(batch_id).Where(e=>!e.ActualDate.Equals(null)).ToList();

            foreach(TblbatchSchedule s in schedule)
            {
                List<AttendanceStudentModel> students = new List<AttendanceStudentModel>();
              List< TblbatchScheduleAttendance>  dlist = attendancerepo.GetAll().Where(e=>e.BatchScheduleId.Equals(s.BatchScheduleId)).ToList();
                TblbatchScheduleAttendance d = dlist.First();
                List<TblscheduleAttendance> attendaces = scheduleattenancerepo.GetAll().Where(e=>e.ScheduleAttendanceId.Equals(d.ScheduleAttendanceId)).ToList();
                foreach(TblscheduleAttendance p in attendaces)
                {
                    TblstudentRegistration r = registrationrepo.GetById((int)p.RegistrationId);
                    TblstudentDetail sd = studentrepo.GetById((int)r.StudentId);
                    AttendanceStudentModel ad = new AttendanceStudentModel() { registration_id= (int)p.RegistrationId, is_present=(int)p.IsPresent, student_id=sd.StudentId, student_name=sd.StudentName};
                    students.Add(ad);
                }
                BatchAttendanceModel b = new BatchAttendanceModel() { batch_id = (int)s.BatchId, actual_date = (DateTime)s.ActualDate, expected_date = (DateTime)s.ExpectedDate, content_id = (int)s.ContentId, content_name = s.Content.ContentName, students=students };
                lst.Add(b);
            }
            return lst;
        }
        public List<BatchStudentAttendanceModel> GetStudentWiseBatchAttendance(int batch_id,int registration_id)
        {
           //  List<BatchAttendanceModel> attendance = GetBatchWiseAttendance(batch_id);
            List<BatchStudentAttendanceModel> lst = new List<BatchStudentAttendanceModel>();

            //foreach(BatchAttendanceModel b in attendance)
            //{
            //    Tblbatch batch = batchrepo.GetById(batch_id);
            //    TbltrainingTopic topic = topicrepo.GetById((int)batch.TopicId);
            //    TblscheduleAttendance sd = scheduleattenancerepo.GetAll().FirstOrDefault(e => e.RegistrationId.Equals(registration_id));
            //    TblstudentRegistration sr = registrationrepo.GetById(registration_id);
            //    TblstudentDetail sdetails = studentrepo.GetById((int)sr.StudentId);
            //    BatchStudentAttendanceModel bm = new BatchStudentAttendanceModel() { 
            //     registration_id=(int)sd.RegistrationId,
            //      is_present= (int)sd.IsPresent,
            //       actual_date=b.actual_date,
            //        expected_date=b.expected_date,
            //         content_id=b.content_id,
            //          content_name=b.content_name,
            //           student_name=sdetails.StudentName,
            //           student_id=sdetails.StudentId,
            //            topic_id=topic.TopicId,
            //             topic_name=topic.TopicName                     
            //    };
            //    lst.Add(bm);

            //}

           
            var query = from b in batchrepo.GetAll()
                        join bs in schedulerepo.GetAll() on b.BatchId equals bs.BatchId
                        where b.BatchId.Equals(batch_id)
                        //join bsa in  db.tblbatch_schedule_attendance  on bs.batch_schedule_id equals bsa.batch_schedule_id
                        //join sd in  db.tblschedule_attendance  on bsa.schedule_attendance_id equals sd.schedule_attendance_id
                        select new
                        {
                            batch_id = b.BatchId,
                            batch_name = b.BatchName,
                            content_id = bs.ContentId,
                            expected_date = bs.ExpectedDate,
                            actual_date = bs.ActualDate,
                            batch_schedule_id = bs.BatchScheduleId,
                        };
            int i = 1;
            foreach (var q in query)
            {
                List<TblbatchScheduleAttendance> attendance = attendancerepo.GetAll().ToList().Where(e => e.BatchScheduleId.Equals(q.batch_schedule_id)).ToList();
                string status = "Not Completed";
                string student_name = "", content_name = "", color = "";
                int reg_id = 1, flag = 0;
                foreach (TblbatchScheduleAttendance a in attendance)
                {

                    List<TblscheduleAttendance> students = scheduleattenancerepo.GetAll().Where(e => e.ScheduleAttendanceId.Equals(a.ScheduleAttendanceId) && e.RegistrationId.Equals(registration_id)).ToList();
                    if (students.Count > 0)
                    {
                        status = "Completed";
                        
                        TblstudentRegistration reg = registrationrepo.GetById((int)students.First().RegistrationId);
                        TblstudentDetail sd = studentrepo.GetById((int)reg.StudentId);
                        flag = (int)students.First().IsPresent;
                        if(flag==1)
                        {
                            status = "Present";
                            color = "green";

                        }
                        else if (flag == 0)
                        {
                            status = "Absent";
                            color = "red";


                        }
                        student_name = sd.StudentName;
                    }

                    //if (q.actual_date != null)
                    //{
                    //    status = "Completed";
                    //}

                    //   Console.WriteLine(a.attendance_date);
                }
                TbltopicContent content = contentrepo.GetById((int)q.content_id);
                TbltrainingTopic tp = topicrepo.GetById((int)content.TopicId);
                content_name = content.ContentName;
                //    Console.WriteLine(i + "\t" + q.content_id + "\t " + content_name + "\t " + student_name + "\t" + q.expected_date + "\t " + q.actual_date + " " + flag + "  " + status);
                string actual_date = "";
                string delay_or_advance = "";
                if (q.actual_date != null)
                {
                    actual_date =q.actual_date.Value.ToShortDateString();
                    if (q.actual_date > q.expected_date) {
                        TimeSpan t = q.actual_date.Value.Subtract(q.expected_date.Value);
                        delay_or_advance = t.Days+" Days Late";
                    }
                    else if(q.expected_date>q.actual_date)
                    {
                        TimeSpan t = q.expected_date.Value.Subtract(q.actual_date.Value);
                        delay_or_advance = t.Days + " Days Early";

                    }
                    else
                    {
                        delay_or_advance =  "On Time";
                    }
                }


                BatchStudentAttendanceModel bm = new BatchStudentAttendanceModel()
                {
                    actual_date = actual_date,
                    content_id = (int)q.content_id,
                    content_name = content_name,
                    expected_date = q.expected_date.Value.ToShortDateString(),
                    is_present = flag,
                    registration_id = registration_id,
                    student_id = 0,
                    student_name = student_name,
                    topic_id = tp.TopicId,
                    topic_name = tp.TopicName,
                    status = status,
                    delay_or_advance = delay_or_advance,
                    color = color

                };
           lst.Add(bm);
            }
            return lst;
        }


        //public TblstudentDetail GetStudentRecord(int student_id)
        //{
        //    TblstudentDetail sd = studentrepo.GetById(student_id);
        //    List<TblstudentRegistration> registrations = registrationrepo.GetAll().Where(e => e.StudentId.Equals(student_id)).ToList();
        //    foreach(TblstudentRegistration r in registrations)
        //    {
        //        TbltrainingCourseFee fee= feerepo.GetById((int)r.FeeId);
        //        TbltrainingCourse course = courserepo.GetById((int)fee.CourseId);
        //        List<TbltrainingCourseTopic> coursetopics = coursetopicrepo.GetAll().Where(e => e.CourseId.Equals(course.CourseId)).ToList();
        //        var query = from t in topicrepo.GetAll()
        //                    join ct in coursetopics on t.TopicId equals ct.TopicId
        //                    join
        //                  b in batchrepo.GetAll() on ct.TopicId equals b.TopicId join tr in trainerrepo.GetAll()
        //                    on b.TrainerId equals tr.TrainerId
        //                    select new {
        //                     batch_id=b.BatchId,
        //                     batch_name=b.BatchName,
        //                     batch_time=b.BatchTime,
        //                     start_date=b.StartDate,
        //                     status=b.Status,
        //                     trainer_id=tr.TrainerId,
        //                      trainer_name=tr.TrainerName
        //                    };
        //        List<Tblbatch> batches = new List<Tblbatch>();
        //        foreach(var q in query)
        //        {
        //            Tbltrainer t = trainerrepo.GetById(q.trainer_id);
        //            Tblbatch b = new Tblbatch() 
        //            {
        //             BatchId=q.batch_id,
        //             BatchName=q.batch_name,
        //              BatchTime=q.batch_time,
        //               StartDate=q.start_date,
        //               Status=q.status,
        //                 TrainerId=q.trainer_id,
        //                  Trainer=t

        //            };
        //            batches.Add(b);
        //        }

        //        fee.Course= course;
        //        r.Fee = fee;

        //    }
        //}

        public List<BatchStudentAttendanceModel> GetStudentWiseBatchDetails(int registration_id)
        {
             
            List<BatchStudentAttendanceModel> lst = new List<BatchStudentAttendanceModel>();
 

            var query = from b in batchrepo.GetAll()
                        join bs in schedulerepo.GetAll() on b.BatchId equals bs.BatchId
                        select new
                        {
                            batch_id = b.BatchId,
                            batch_name = b.BatchName,
                            content_id = bs.ContentId,
                            expected_date = bs.ExpectedDate,
                            actual_date = bs.ActualDate,
                            batch_schedule_id = bs.BatchScheduleId,
                        };
            int i = 1;
            foreach (var q in query)
            {
                List<TblbatchScheduleAttendance> attendance = attendancerepo.GetAll().ToList().Where(e => e.BatchScheduleId.Equals(q.batch_schedule_id)).ToList();
                string status = "Not Completed";
                string student_name = "", content_name = "", color = "";
                int reg_id = 1, flag = 0;
                foreach (TblbatchScheduleAttendance a in attendance)
                {

                    List<TblscheduleAttendance> students = scheduleattenancerepo.GetAll().Where(e => e.ScheduleAttendanceId.Equals(a.ScheduleAttendanceId) && e.RegistrationId.Equals(registration_id)).ToList();
                    if (students.Count > 0)
                    {
                        status = "Completed";

                        TblstudentRegistration reg = registrationrepo.GetById((int)students.First().RegistrationId);
                        TblstudentDetail sd = studentrepo.GetById((int)reg.StudentId);
                        flag = (int)students.First().IsPresent;
                        if (flag == 1)
                        {
                            status = "Present";
                            color = "green";

                        }
                        else if (flag == 0)
                        {
                            status = "Absent";
                            color = "red";


                        }
                        student_name = sd.StudentName;
                    }

                }
                TbltopicContent content = contentrepo.GetById((int)q.content_id);
                TbltrainingTopic tp = topicrepo.GetById((int)content.TopicId);
                content_name = content.ContentName;
                //    Console.WriteLine(i + "\t" + q.content_id + "\t " + content_name + "\t " + student_name + "\t" + q.expected_date + "\t " + q.actual_date + " " + flag + "  " + status);
                string actual_date = "";
                string delay_or_advance = "";
                if (q.actual_date != null)
                {
                    actual_date = q.actual_date.Value.ToShortDateString();
                    if (q.actual_date > q.expected_date)
                    {
                        TimeSpan t = q.actual_date.Value.Subtract(q.expected_date.Value);
                        delay_or_advance = t.Days + " Days Late";
                    }
                    else if (q.expected_date > q.actual_date)
                    {
                        TimeSpan t = q.expected_date.Value.Subtract(q.actual_date.Value);
                        delay_or_advance = t.Days + " Days Early";

                    }
                    else
                    {
                        delay_or_advance = "On Time";
                    }
                }


                BatchStudentAttendanceModel bm = new BatchStudentAttendanceModel()
                {
                    actual_date = actual_date,
                    content_id = (int)q.content_id,
                    content_name = content_name,
                    expected_date = q.expected_date.Value.ToShortDateString(),
                    is_present = flag,
                    registration_id = registration_id,
                    student_id = 0,
                    student_name = student_name,
                    topic_id = tp.TopicId,
                    topic_name = tp.TopicName,
                    status = status,
                    delay_or_advance = delay_or_advance,
                    color = color

                };
                lst.Add(bm);
            }
            return lst;
        }


        public List<Tblbatch>GetStudentWiseBatchdetails(int registration_id)
        {
            List<Tblbatch> lst = new List<Tblbatch>();
            var query = from b in batchrepo.GetAll() join bs in batchstudentrepo.GetAll() on b.BatchId equals
                      bs.BatchId join r in registrationrepo.GetAll() on bs.RegistrationId equals r.RegistrationId
                        join tr in trainerrepo.GetAll() on b.TrainerId equals tr.TrainerId
                        join tp in topicrepo.GetAll() on b.TopicId equals tp.TopicId
                        where r.RegistrationId.Equals(registration_id) select new
                        {
                            batch = b,
                            trainer=tr,
                            topic=tp

                      };
            foreach(var q in query)
            {
                List<TblbatchSchedule> schedule = schedulerepo.GetAll().Where(e => e.BatchId.Equals(q.batch.BatchId)).ToList();
                List<TblbatchSchedule> scheduledata = new List<TblbatchSchedule>();
                foreach (TblbatchSchedule s in schedule)
                {
                    List<TblbatchScheduleAttendance> listdata = attendancerepo.GetAll().Where(e => e.BatchScheduleId.Equals(s.BatchScheduleId)).ToList();
                    List<TblbatchScheduleAttendance> attendancedata = new List<TblbatchScheduleAttendance>();
                    foreach(TblbatchScheduleAttendance bd in listdata)
                    {
                        List<TblscheduleAttendance> attendance = scheduleattenancerepo.GetAll().Where(e => e.ScheduleAttendanceId.Equals(bd.ScheduleAttendanceId) && e.RegistrationId.Equals(registration_id)).ToList();
                        bd.TblscheduleAttendances=attendance;
                        attendancedata.Add(bd);


                    }
                    s.TblbatchScheduleAttendances= attendancedata;
                scheduledata.Add(s);
                }
                TbltrainingTopic topic = q.topic;
                List<TbltopicContent> contents=contentrepo.GetAll().Where(e=>e.TopicId.Equals(topic.TopicId)).ToList(); 
                topic.TbltopicContents=contents;
                Tblbatch b = new Tblbatch()
                {
                    BatchId = q.batch.BatchId,
                    BatchName = q.batch.BatchName,
                    BatchTime = q.batch.BatchTime,
                    EndDate = q.batch.EndDate,
                    StartDate = q.batch.StartDate,
                    Status = q.batch.Status,
                    TopicId = q.batch.TopicId,
                    TrainerId = q.batch.TrainerId,
                    Trainer = q.trainer,
                    Topic = topic,
                    TblbatchSchedules = scheduledata
                };
                lst.Add(b);
            }
            return lst;
        }

    }
}