using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCCore_BatchManagementSystemProject.Models;

public partial class BatchManagementDbContext : DbContext
{
    public BatchManagementDbContext()
    {
    }

    public BatchManagementDbContext(DbContextOptions<BatchManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbladminDetail> TbladminDetails { get; set; }

    public virtual DbSet<Tblbatch> Tblbatches { get; set; }

    public virtual DbSet<TblbatchSchedule> TblbatchSchedules { get; set; }

    public virtual DbSet<TblbatchScheduleAttendance> TblbatchScheduleAttendances { get; set; }

    public virtual DbSet<TblbatchStudent> TblbatchStudents { get; set; }

    public virtual DbSet<Tblholiday> Tblholidays { get; set; }

    public virtual DbSet<TblscheduleAttendance> TblscheduleAttendances { get; set; }

    public virtual DbSet<TblstudentDetail> TblstudentDetails { get; set; }

    public virtual DbSet<TblstudentPayment> TblstudentPayments { get; set; }

    public virtual DbSet<TblstudentRegistration> TblstudentRegistrations { get; set; }

    public virtual DbSet<TbltopicContent> TbltopicContents { get; set; }

    public virtual DbSet<Tbltrainer> Tbltrainers { get; set; }

    public virtual DbSet<TbltrainerTopic> TbltrainerTopics { get; set; }

    public virtual DbSet<TbltrainingCourse> TbltrainingCourses { get; set; }

    public virtual DbSet<TbltrainingCourseFee> TbltrainingCourseFees { get; set; }

    public virtual DbSet<TbltrainingCourseTopic> TbltrainingCourseTopics { get; set; }

    public virtual DbSet<TbltrainingTopic> TbltrainingTopics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=A2NWPLSK14SQL-v04.shr.prod.iad2.secureserver.net;user=batchuser;password=ciit@2023;Database=batch_managementDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("batchuser");

        modelBuilder.Entity<TbladminDetail>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__tbladmin__43AA41414A6F90DF");

            entity.ToTable("tbladmin_details", "dbo");

            entity.HasIndex(e => e.UserName, "UQ__tbladmin__7C9273C4ECB8FDBA").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<Tblbatch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__tblbatch__DBFC043189FE4B56");

            entity.ToTable("tblbatches", "dbo");

            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.BatchName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("batch_name");
            entity.Property(e => e.BatchTime)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("batch_time");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Topic).WithMany(p => p.Tblbatches)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("fktopsdicsasd");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Tblbatches)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("fktzdsdsopicsasd");
        });

        modelBuilder.Entity<TblbatchSchedule>(entity =>
        {
            entity.HasKey(e => e.BatchScheduleId).HasName("PK__tblbatch__2C18A81358C99661");

            entity.ToTable("tblbatch_schedule", "dbo");

            entity.Property(e => e.BatchScheduleId).HasColumnName("batch_schedule_id");
            entity.Property(e => e.ActualDate)
                .HasColumnType("date")
                .HasColumnName("actual_date");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.ContentId).HasColumnName("content_id");
            entity.Property(e => e.ExpectedDate)
                .HasColumnType("date")
                .HasColumnName("expected_date");

            entity.HasOne(d => d.Batch).WithMany(p => p.TblbatchSchedules)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("fsdkbatchpicsasd");

            entity.HasOne(d => d.Content).WithMany(p => p.TblbatchSchedules)
                .HasForeignKey(d => d.ContentId)
                .HasConstraintName("fcontenticsasd");
        });

        modelBuilder.Entity<TblbatchScheduleAttendance>(entity =>
        {
            entity.HasKey(e => e.ScheduleAttendanceId).HasName("PK__tblbatch__3DBF72ADF6C2C59A");

            entity.ToTable("tblbatch_schedule_attendance", "dbo");

            entity.Property(e => e.ScheduleAttendanceId).HasColumnName("schedule_attendance_id");
            entity.Property(e => e.AttendanceDate)
                .HasColumnType("date")
                .HasColumnName("attendance_date");
            entity.Property(e => e.BatchScheduleId).HasColumnName("batch_schedule_id");

            entity.HasOne(d => d.BatchSchedule).WithMany(p => p.TblbatchScheduleAttendances)
                .HasForeignKey(d => d.BatchScheduleId)
                .HasConstraintName("fkscheid");
        });

        modelBuilder.Entity<TblbatchStudent>(entity =>
        {
            entity.HasKey(e => e.BatchStudentId).HasName("PK__tblbatch__27C6D4A6E8622748");

            entity.ToTable("tblbatch_students", "dbo");

            entity.Property(e => e.BatchStudentId).HasColumnName("batch_student_id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");

            entity.HasOne(d => d.Batch).WithMany(p => p.TblbatchStudents)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("fsdktopicsasd");

            entity.HasOne(d => d.Registration).WithMany(p => p.TblbatchStudents)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("fkregsopicsasd");
        });

        modelBuilder.Entity<Tblholiday>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("PK__tblholid__253884EA45AE370A");

            entity.ToTable("tblholidays", "dbo");

            entity.Property(e => e.HolidayId).HasColumnName("holiday_id");
            entity.Property(e => e.HolidayDate)
                .HasColumnType("date")
                .HasColumnName("holiday_date");
            entity.Property(e => e.HolidayPurpose)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("holiday_purpose");
        });

        modelBuilder.Entity<TblscheduleAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__tblsched__20D6A9682CDB87D3");

            entity.ToTable("tblschedule_attendance", "dbo");

            entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");
            entity.Property(e => e.IsPresent)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_present");
            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");
            entity.Property(e => e.ScheduleAttendanceId).HasColumnName("schedule_attendance_id");

            entity.HasOne(d => d.Registration).WithMany(p => p.TblscheduleAttendances)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("fkregasdheid");

            entity.HasOne(d => d.ScheduleAttendance).WithMany(p => p.TblscheduleAttendances)
                .HasForeignKey(d => d.ScheduleAttendanceId)
                .HasConstraintName("fkscasdheid");
        });

        modelBuilder.Entity<TblstudentDetail>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__tblstude__2A33069AF9234940");

            entity.ToTable("tblstudent_details", "dbo");

            entity.HasIndex(e => e.EmailAddress, "UQ__tblstude__20C6DFF586C0C5AC").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mobile_number");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePhoto)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("profile_photo");
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("qualification");
            entity.Property(e => e.StudentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("student_name");
        });

        modelBuilder.Entity<TblstudentPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__tblstude__ED1FC9EAB597A473");

            entity.ToTable("tblstudent_payments", "dbo");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PaymentAmount).HasColumnName("payment_amount");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentDescription)
                .IsUnicode(false)
                .HasColumnName("payment_description");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("payment_mode");
            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");

            entity.HasOne(d => d.Registration).WithMany(p => p.TblstudentPayments)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("fkregid");
        });

        modelBuilder.Entity<TblstudentRegistration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__tblstude__22A298F6DBD821BA");

            entity.ToTable("tblstudent_registrations", "dbo");

            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.FeeId).HasColumnName("fee_id");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("date")
                .HasColumnName("registration_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Fee).WithMany(p => p.TblstudentRegistrations)
                .HasForeignKey(d => d.FeeId)
                .HasConstraintName("fkfeesid");

            entity.HasOne(d => d.Student).WithMany(p => p.TblstudentRegistrations)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fkstidsd");
        });

        modelBuilder.Entity<TbltopicContent>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__tbltopic__655FE5100B6BC2BE");

            entity.ToTable("tbltopic_contents", "dbo");

            entity.Property(e => e.ContentId).HasColumnName("content_id");
            entity.Property(e => e.ContentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("content_name");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Topic).WithMany(p => p.TbltopicContents)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("fktopicid");
        });

        modelBuilder.Entity<Tbltrainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__tbltrain__65A4B629F2E1139C");

            entity.ToTable("tbltrainers", "dbo");

            entity.HasIndex(e => e.EmailAddress, "UQ__tbltrain__20C6DFF58987F3A3").IsUnique();

            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mobile_number");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePhoto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("profile_photo");
            entity.Property(e => e.Qualification)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("qualification");
            entity.Property(e => e.TrainerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("trainer_name");
        });

        modelBuilder.Entity<TbltrainerTopic>(entity =>
        {
            entity.HasKey(e => e.TrainerTopicId).HasName("PK__tbltrain__B6832449807C9A1F");

            entity.ToTable("tbltrainer_topics", "dbo");

            entity.Property(e => e.TrainerTopicId).HasColumnName("trainer_topic_id");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Topic).WithMany(p => p.TbltrainerTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("fktopicsasd");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TbltrainerTopics)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("fktzdsopicsasd");
        });

        modelBuilder.Entity<TbltrainingCourse>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__tbltrain__8F1EF7AE16FF7232");

            entity.ToTable("tbltraining_courses", "dbo");

            entity.HasIndex(e => e.CourseName, "UQ__tbltrain__B5B2A66A8B1D9E62").IsUnique();

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course_name");
        });

        modelBuilder.Entity<TbltrainingCourseFee>(entity =>
        {
            entity.HasKey(e => e.FeeId).HasName("PK__tbltrain__A19C8AFBA5681CC2");

            entity.ToTable("tbltraining_course_fees", "dbo");

            entity.Property(e => e.FeeId).HasColumnName("fee_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.FeeMode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fee_mode");
            entity.Property(e => e.FeesAmount).HasColumnName("fees_amount");
            entity.Property(e => e.Gst).HasColumnName("gst");

            entity.HasOne(d => d.Course).WithMany(p => p.TbltrainingCourseFees)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fkcourseid");
        });

        modelBuilder.Entity<TbltrainingCourseTopic>(entity =>
        {
            entity.HasKey(e => e.CourseTopicId).HasName("PK__tbltrain__564DD4A360E9B742");

            entity.ToTable("tbltraining_course_topics", "dbo");

            entity.Property(e => e.CourseTopicId).HasColumnName("course_topic_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Course).WithMany(p => p.TbltrainingCourseTopics)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fkcodi");

            entity.HasOne(d => d.Topic).WithMany(p => p.TbltrainingCourseTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("fkcozdfdi");
        });

        modelBuilder.Entity<TbltrainingTopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__tbltrain__D5DAA3E9E404980F");

            entity.ToTable("tbltraining_topics", "dbo");

            entity.HasIndex(e => e.TopicName, "UQ__tbltrain__54BAE5ECFFD503E1").IsUnique();

            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.TopicName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("topic_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
