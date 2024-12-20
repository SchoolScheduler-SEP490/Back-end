﻿using Microsoft.EntityFrameworkCore;
using SchedulifySystem.Repository.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.DBContext;

public partial class SchedulifyContext : DbContext
{
    public SchedulifyContext()
    {

    }

    public SchedulifyContext(DbContextOptions<SchedulifyContext> options) : base(options)
    {

    }

    public virtual DbSet<Account> Accounts { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<ClassPeriod> ClassPeriods { get; set; }
    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<SchoolSchedule> SchoolSchedules { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentClassGroup> StudentClassGroups { get; set; }
    public DbSet<CurriculumDetail> CurriculumDetails { get; set; }
    public DbSet<TeachableSubject> TeachableSubjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherAssignment> TeacherAssignments { get; set; }
    public DbSet<TeacherUnavailability> TeacherUnavailabilities { get; set; }
    public DbSet<Term> Terms { get; set; }
    public DbSet<RoleAssignment> RoleAssignments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<OTP> OTPs { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<SubmitRequest> SubmitsRequests { get; set; }
    public DbSet<RoomSubject> RoomSubjects { get; set; }
    public DbSet<Curriculum> Curriculums { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Account Entity
        modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);
        modelBuilder.Entity<Account>()
                .ToTable("Account");
        modelBuilder.Entity<Account>()
            .Property(a => a.FirstName)
            .HasMaxLength(100);
        modelBuilder.Entity<Account>()
            .Property(a => a.LastName)
            .HasMaxLength(100);
        modelBuilder.Entity<Account>()
            .Property(a => a.Password)
            .HasMaxLength(100);
        modelBuilder.Entity<Account>()
            .HasOne(b => b.School)
            .WithMany(s => s.Accounts)
            .HasForeignKey(b => b.SchoolId);
        modelBuilder.Entity<Account>()
            .HasOne(b => b.School)
            .WithMany(s => s.Accounts)
            .HasForeignKey(b => b.SchoolId);

        // RoleAssignment Entity
        modelBuilder.Entity<RoleAssignment>()
                .HasKey(a => a.Id);
        modelBuilder.Entity<RoleAssignment>()
                .ToTable("RoleAssignment");
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(b => b.Account)
            .WithMany(s => s.RoleAssignments)
            .HasForeignKey(b => b.AccountId);
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(b => b.Role)
            .WithMany(s => s.RoleAssignments)
            .HasForeignKey(b => b.RoleId);
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(b => b.Department)
            .WithMany(s => s.RoleAssignments)
            .HasForeignKey(b => b.DepartmentId);

        // Notification Entity
        modelBuilder.Entity<Notification>()
                .HasKey(a => a.Id);
        modelBuilder.Entity<Notification>()
                .ToTable("Notification");
        modelBuilder.Entity<Notification>()
            .Property(a => a.Title)
            .HasMaxLength(150);
        modelBuilder.Entity<Notification>()
            .Property(a => a.Message)
            .HasMaxLength(350);
        modelBuilder.Entity<Notification>()
            .HasOne(b => b.Account)
            .WithMany(s => s.Notifications)
            .HasForeignKey(b => b.AccountId);

        modelBuilder.Entity<OTP>()
            .HasOne(b => b.Account)
            .WithMany(s => s.OTPs)
            .HasForeignKey(b => b.AccountId);

        // Building Entity
        modelBuilder.Entity<Building>()
            .ToTable("Building");
        modelBuilder.Entity<Building>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Building>()
            .Property(b => b.Name)
            .HasMaxLength(50);
        modelBuilder.Entity<Building>()
            .Property(b => b.Description)
            .HasMaxLength(250);
        modelBuilder.Entity<Building>()
            .HasOne(b => b.School)
            .WithMany(s => s.Buildings)
            .HasForeignKey(b => b.SchoolId);


        // ClassPeriod Entity
        modelBuilder.Entity<ClassPeriod>(entity =>
        {
            entity.ToTable("ClassPeriod");
            entity.HasKey(cp => cp.Id);

            entity.HasOne(cp => cp.ClassSchedule)
                .WithMany(cs => cs.ClassPeriods)
                .HasForeignKey(cp => cp.ClassScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(cp => cp.Room)
                .WithMany(r => r.ClassPeriods)
                .HasForeignKey(cp => cp.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cp => cp.Teacher)
                .WithMany(t => t.ClassPeriods)
                .HasForeignKey(cp => cp.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cp => cp.Subject)
                .WithMany(s => s.ClassPeriods)
                .HasForeignKey(cp => cp.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        // ClassSchedule Entity
        modelBuilder.Entity<ClassSchedule>()
            .ToTable("ClassSchedule");
        modelBuilder.Entity<ClassSchedule>()
            .HasKey(cs => cs.Id);
        modelBuilder.Entity<ClassSchedule>()
            .Property(ca => ca.Name)
            .HasMaxLength(70);
        modelBuilder.Entity<ClassSchedule>()
            .HasOne(cs => cs.SchoolSchedule)
            .WithMany(ss => ss.ClassSchedules)
            .HasForeignKey(cs => cs.SchoolScheduleId)
            .OnDelete(DeleteBehavior.Cascade);


        // Department Entity
        modelBuilder.Entity<Department>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .HasMaxLength(100);
        modelBuilder.Entity<Department>()
            .HasOne(d => d.School)
            .WithMany(s => s.Departments)
            .HasForeignKey(d => d.SchoolId);

        // Room Entity
        modelBuilder.Entity<Room>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<Room>()
            .Property(r => r.Name)
            .HasMaxLength(50);
        modelBuilder.Entity<Room>()
            .HasOne(r => r.Building)
            .WithMany(b => b.Rooms)
            .HasForeignKey(r => r.BuildingId);


        // School Entity
        modelBuilder.Entity<School>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<School>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<School>()
            .HasOne(ed => ed.Province)
            .WithMany(p => p.Schools)
            .HasForeignKey(ed => ed.ProvinceId);

        // SchoolSchedule Entity
        modelBuilder.Entity<SchoolSchedule>()
            .HasKey(ss => ss.Id);
        modelBuilder.Entity<SchoolSchedule>()
            .HasOne(ss => ss.School)
            .WithMany(s => s.SchoolSchedules)
            .HasForeignKey(ss => ss.SchoolId);
        modelBuilder.Entity<SchoolSchedule>()
            .HasOne(ss => ss.Term)
            .WithMany(t => t.SchoolSchedules)
            .HasForeignKey(ss => ss.TermId);
        modelBuilder.Entity<SchoolSchedule>()
            .HasOne(ss => ss.SchoolYear)
            .WithMany(s => s.SchoolSchedules)
            .HasForeignKey(ss => ss.SchoolYearId);

        // SchoolYear Entity
        modelBuilder.Entity<SchoolYear>()
            .HasKey(sy => sy.Id);

        // StudentClass Entity
        modelBuilder.Entity<StudentClass>()
            .HasKey(sc => sc.Id);
        modelBuilder.Entity<StudentClass>()
            .Property(sc => sc.Name)
            .HasMaxLength(50);
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.SchoolYear)
            .WithMany(t => t.StudentClasses)
            .HasForeignKey(sc => sc.SchoolYearId);
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.School)
            .WithMany(t => t.StudentClasses)
            .HasForeignKey(sc => sc.SchoolId);
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Teacher)
            .WithMany(t => t.StudentClasses)
            .HasForeignKey(sc => sc.HomeroomTeacherId)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.StudentClassGroup)
            .WithMany(t => t.StudentClasses)
            .HasForeignKey(sc => sc.StudentClassGroupId);
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Room)
            .WithMany(t => t.StudentClasses)
            .HasForeignKey(sc => sc.RoomId)
            .OnDelete(DeleteBehavior.SetNull);


        // Subject Entity
        modelBuilder.Entity<Subject>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Subject>()
            .Property(s => s.SubjectName)
            .HasMaxLength(100);
        modelBuilder.Entity<Subject>()
            .Property(s => s.Abbreviation)
            .HasMaxLength(50);
        modelBuilder.Entity<Subject>()
            .Property(s => s.Description)
            .HasMaxLength(150);
        modelBuilder.Entity<Subject>()
            .HasOne(sc => sc.SchoolYear)
            .WithMany(t => t.Subjects)
            .HasForeignKey(sc => sc.SchoolYearId);

        // Student Class Group Entity
        modelBuilder.Entity<CurriculumDetail>()
            .ToTable("StudentClassGroup");
        modelBuilder.Entity<StudentClassGroup>()
            .HasKey(sg => sg.Id);
        modelBuilder.Entity<StudentClassGroup>()
            .Property(sg => sg.GroupName)
            .HasMaxLength(100);
        modelBuilder.Entity<StudentClassGroup>()
            .HasOne(sig => sig.School)
            .WithMany(sg => sg.StudentClassGroups)
            .HasForeignKey(sig => sig.SchoolId);
        modelBuilder.Entity<StudentClassGroup>()
           .HasOne(sig => sig.SchoolYear)
           .WithMany(sg => sg.StudentClassGroups)
           .HasForeignKey(sig => sig.SchoolYearId);
        modelBuilder.Entity<StudentClassGroup>()
           .HasOne(sig => sig.Curriculum)
           .WithMany(sg => sg.StudentClassGroups)
           .HasForeignKey(sig => sig.CurriculumId);

        // Curriculum Detail Entity
        modelBuilder.Entity<CurriculumDetail>()
            .ToTable("CurriculumDetail");
        modelBuilder.Entity<CurriculumDetail>()
            .HasKey(sig => sig.Id);
        modelBuilder.Entity<CurriculumDetail>()
            .HasOne(sig => sig.Subject)
            .WithMany(s => s.CurriculumDetails)
            .HasForeignKey(sig => sig.SubjectId);
        modelBuilder.Entity<CurriculumDetail>()
            .HasOne(sig => sig.Term)
            .WithMany(t => t.CurriculumDetails)
            .HasForeignKey(sig => sig.TermId);
        modelBuilder.Entity<CurriculumDetail>()
            .HasOne(sig => sig.Curriculum)
            .WithMany(t => t.CurriculumDetails)
            .HasForeignKey(sig => sig.CurriculumId);

        // TeachableSubject Entity
        modelBuilder.Entity<TeachableSubject>()
            .HasKey(ts => ts.Id);
        modelBuilder.Entity<TeachableSubject>()
            .HasOne(ts => ts.Teacher)
            .WithMany(t => t.TeachableSubjects)
            .HasForeignKey(ts => ts.TeacherId);
        modelBuilder.Entity<TeachableSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.TeachableSubjects)
            .HasForeignKey(ts => ts.SubjectId);

        // Teacher Entity
        modelBuilder.Entity<Teacher>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<Teacher>()
            .Property(t => t.FirstName)
            .HasMaxLength(100);
        modelBuilder.Entity<Teacher>()
            .Property(t => t.LastName)
            .HasMaxLength(100);
        modelBuilder.Entity<Teacher>()
            .Property(t => t.Abbreviation)
            .HasMaxLength(50);
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Department)
            .WithMany(d => d.Teachers)
            .HasForeignKey(t => t.DepartmentId);
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.School)
            .WithMany(d => d.Teachers)
            .HasForeignKey(t => t.SchoolId);

        //TeacherAssignment Entity
        modelBuilder.Entity<TeacherAssignment>(entity =>
        {
            entity.HasKey(ta => ta.Id);

            entity.HasOne(ta => ta.Teacher)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(ta => ta.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);


            entity.HasOne(ta => ta.Subject)
               .WithMany(t => t.TeacherAssignments)
               .HasForeignKey(ta => ta.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ta => ta.StudentClass)
                .WithMany(sc => sc.TeacherAssignments)
                .HasForeignKey(ta => ta.StudentClassId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(ta => ta.Term)
                .WithMany(sc => sc.TeacherAssignments)
                .HasForeignKey(ta => ta.TermId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ta => ta.RoomSubject)
                .WithMany(sc => sc.TeacherAssignments)
                .HasForeignKey(ta => ta.RoomSubjectId)
                .OnDelete(DeleteBehavior.Cascade);

        });



        // TeacherUnavailability Entity
        modelBuilder.Entity<TeacherUnavailability>()
            .HasKey(tu => tu.Id);
        modelBuilder.Entity<TeacherUnavailability>()
            .HasOne(tu => tu.Teacher)
            .WithMany(t => t.TeacherUnavailabilities)
            .HasForeignKey(tu => tu.TeacherId);

        // Term Entity
        modelBuilder.Entity<Term>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<Term>()
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Term>()
            .HasOne(t => t.SchoolYear)
            .WithMany(sy => sy.Terms)
            .HasForeignKey(t => t.SchoolYearId);

        // district Entity
        modelBuilder.Entity<District>()
            .HasKey(ed => ed.Id);
        modelBuilder.Entity<District>()
            .Property(ts => ts.Name)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<District>()
            .HasOne(ed => ed.Province)
            .WithMany(p => p.Districts)
            .HasForeignKey(ed => ed.ProvinceId);

        // Province Entity
        modelBuilder.Entity<Province>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Province>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        // Role Entity
        modelBuilder.Entity<Role>()
            .ToTable("Role");
        modelBuilder.Entity<Role>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Role>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        // SubmitRequest Entity
        modelBuilder.Entity<SubmitRequest>()
            .HasKey(sr => sr.Id);
        modelBuilder.Entity<SubmitRequest>()
            .HasOne(sr => sr.Teacher)
            .WithMany(t => t.SubmitRequests)
            .HasForeignKey(sr => sr.TeacherId);
        modelBuilder.Entity<SubmitRequest>()
            .HasOne(sr => sr.SchoolYear)
            .WithMany(t => t.SubmitRequests)
            .HasForeignKey(sr => sr.SchoolYearId);

        //RoomSubject
        modelBuilder.Entity<RoomSubject>()
            .HasKey(rs => rs.Id);
        modelBuilder.Entity<RoomSubject>()
            .HasOne(rs => rs.Subject)
            .WithMany(s => s.RoomSubjects)
            .HasForeignKey(rs => rs.SubjectId);
        modelBuilder.Entity<RoomSubject>()
           .HasOne(rs => rs.Room)
           .WithMany(r => r.RoomSubjects)
           .HasForeignKey(rs => rs.RoomId);
        modelBuilder.Entity<RoomSubject>()
           .HasOne(rs => rs.School)
           .WithMany(r => r.RoomSubjects)
           .HasForeignKey(rs => rs.SchoolId);
        modelBuilder.Entity<RoomSubject>()
           .HasOne(rs => rs.Term)
           .WithMany(r => r.RoomSubjects)
           .HasForeignKey(rs => rs.TermId);
        modelBuilder.Entity<RoomSubject>()
           .HasOne(rs => rs.Teacher)
           .WithMany(r => r.RoomSubjects)
           .HasForeignKey(rs => rs.TeacherId);

        //Curriculum
        modelBuilder.Entity<Curriculum>()
            .HasKey(rs => rs.Id);
        modelBuilder.Entity<Curriculum>()
            .HasOne(rs => rs.School)
            .WithMany(s => s.Curriculums)
            .HasForeignKey(rs => rs.SchoolId);
        modelBuilder.Entity<Curriculum>()
            .HasOne(rs => rs.SchoolYear)
            .WithMany(s => s.Curriculums)
            .HasForeignKey(rs => rs.SchoolYearId);

        //Student Class Room Subject
        modelBuilder.Entity<StudentClassRoomSubject>()
            .HasKey(rs => rs.Id);
        modelBuilder.Entity<StudentClassRoomSubject>()
            .HasOne(rs => rs.StudentClass)
            .WithMany(s => s.StudentClassRoomSubjects)
            .HasForeignKey(rs => rs.StudentClassId);

        modelBuilder.Entity<StudentClassRoomSubject>()
            .HasOne(rs => rs.RoomSubject)
            .WithMany(s => s.StudentClassRoomSubjects)
            .HasForeignKey(rs => rs.RoomSubjectId);

        // Period Change
        modelBuilder.Entity<PeriodChange>()
            .HasKey(pc => pc.Id);

        modelBuilder.Entity<PeriodChange>()
            .HasOne(pc => pc.ClassPeriod)
            .WithMany(cp => cp.PeriodChanges)
            .HasForeignKey(pc => pc.ClassPeriodId);

    }
}

