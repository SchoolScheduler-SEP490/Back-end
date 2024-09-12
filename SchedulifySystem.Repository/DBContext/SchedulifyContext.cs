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

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<ClassGroup> ClassGroups { get; set; }
    public DbSet<ClassPeriod> ClassPeriods { get; set; }
    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<ConfigAttribute> ConfigAttributes { get; set; }
    public DbSet<Curriculum> Curriculums { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<ScheduleConfig> ScheduleConfigs { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<SchoolSchedule> SchoolSchedules { get; set; }
    public DbSet<SchoolYear> SchoolYears { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<StudentClassInGroup> StudentClassInGroups { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectConfig> SubjectConfigs { get; set; }
    public DbSet<SubjectGroup> SubjectGroups { get; set; }
    public DbSet<SubjectInGroup> SubjectInGroups { get; set; }
    public DbSet<TeachableSubject> TeachableSubjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherAssignment> TeacherAssignments { get; set; }
    public DbSet<TeacherConfig> TeacherConfigs { get; set; }
    public DbSet<TeacherGroup> TeacherGroups { get; set; }
    public DbSet<TeacherUnavailability> TeacherUnavailabilities { get; set; }
    public DbSet<Term> Terms { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Account Entity
        modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);
        modelBuilder.Entity<Account>()
            .Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Account>()
            .Property(a => a.Password)
            .IsRequired()
            .HasMaxLength(100);

        // Building Entity
        modelBuilder.Entity<Building>()
            .HasKey(b => b.Id);
        modelBuilder.Entity<Building>()
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<Building>()
            .HasOne(b => b.School)
            .WithMany(s => s.Buildings)
            .HasForeignKey(b => b.SchoolId);

        // ClassGroup Entity
        modelBuilder.Entity<ClassGroup>()
            .HasKey(cg => cg.Id);
        modelBuilder.Entity<ClassGroup>()
            .Property(cg => cg.Name)
            .IsRequired()
            .HasMaxLength(50);

        // ClassPeriod Entity
        modelBuilder.Entity<ClassPeriod>()
            .HasKey(cp => cp.Id);
        modelBuilder.Entity<ClassPeriod>()
            .HasOne(cp => cp.ClassSchedule)
            .WithMany(cs => cs.ClassPeriods)
            .HasForeignKey(cp => cp.ClassScheduleId);

        // ClassSchedule Entity
        modelBuilder.Entity<ClassSchedule>()
            .HasKey(cs => cs.Id);
        modelBuilder.Entity<ClassSchedule>()
            .HasOne(cs => cs.SchoolSchedule)
            .WithMany(ss => ss.ClassSchedules)
            .HasForeignKey(cs => cs.SchoolScheduleId);

        // ConfigAttribute Entity
        modelBuilder.Entity<ConfigAttribute>()
            .HasKey(ca => ca.Id);
        modelBuilder.Entity<ConfigAttribute>()
            .Property(ca => ca.Name)
            .IsRequired()
            .HasMaxLength(50);

        // Curriculum Entity
        modelBuilder.Entity<Curriculum>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Curriculum>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<Curriculum>()
            .HasOne(c => c.SchoolYear)
            .WithMany(sy => sy.Curriculums)
            .HasForeignKey(c => c.SchoolYearId);

        // Department Entity
        modelBuilder.Entity<Department>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .IsRequired()
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
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Room>()
            .HasOne(r => r.Building)
            .WithMany(b => b.Rooms)
            .HasForeignKey(r => r.BuildingId);
        modelBuilder.Entity<Room>()
            .HasOne(r => r.RoomType)
            .WithMany(rt => rt.Rooms)
            .HasForeignKey(r => r.RoomTypeId);

        // RoomType Entity
        modelBuilder.Entity<RoomType>()
            .HasKey(rt => rt.Id);
        modelBuilder.Entity<RoomType>()
            .Property(rt => rt.Name)
            .IsRequired()
            .HasMaxLength(50);

        // ScheduleConfig Entity
        modelBuilder.Entity<ScheduleConfig>()
            .HasKey(sc => sc.Id);
        modelBuilder.Entity<ScheduleConfig>()
            .HasOne(sc => sc.ConfigAttribute)
            .WithMany(ca => ca.ScheduleConfigs)
            .HasForeignKey(sc => sc.ConfigAttributeId);

        // School Entity
        modelBuilder.Entity<School>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<School>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

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

        // SchoolYear Entity
        modelBuilder.Entity<SchoolYear>()
            .HasKey(sy => sy.Id);

        // StudentClass Entity
        modelBuilder.Entity<StudentClass>()
            .HasKey(sc => sc.Id);
        modelBuilder.Entity<StudentClass>()
            .Property(sc => sc.Name)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<StudentClass>()
            .HasOne(sc => sc.Teacher)
            .WithMany(t => t.StudentClasses)
            .HasForeignKey(sc => sc.HomeroomTeacherId);


        // StudentClassInGroup Entity
        modelBuilder.Entity<StudentClassInGroup>()
            .HasKey(scg => scg.Id);
        modelBuilder.Entity<StudentClassInGroup>()
            .HasOne(scg => scg.StudentClass)
            .WithMany(sc => sc.StudentClassInGroups)
            .HasForeignKey(scg => scg.StudentClassId);
        modelBuilder.Entity<StudentClassInGroup>()
            .HasOne(scg => scg.ClassGroup)
            .WithMany(cg => cg.StudentClassInGroups)
            .HasForeignKey(scg => scg.ClassGroupId);

        // Subject Entity
        modelBuilder.Entity<Subject>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Subject>()
            .Property(s => s.SubjectName)
            .HasMaxLength(100);
        modelBuilder.Entity<Subject>()
            .Property(s => s.Description)
            .HasMaxLength(150);

        // SubjectConfig Entity
        modelBuilder.Entity<SubjectConfig>()
            .HasKey(sc => sc.Id);
        modelBuilder.Entity<SubjectConfig>()
            .HasOne(sc => sc.ConfigAttribute)
            .WithMany(ca => ca.SubjectConfigs)
            .HasForeignKey(sc => sc.ConfigAttributeId);

        // SubjectGroup Entity
        modelBuilder.Entity<SubjectGroup>()
            .HasKey(sg => sg.Id);
        modelBuilder.Entity<SubjectGroup>()
            .Property(sg => sg.GroupName)
            .HasMaxLength(100);

        // SubjectInGroup Entity
        modelBuilder.Entity<SubjectInGroup>()
            .HasKey(sig => sig.Id);
        modelBuilder.Entity<SubjectInGroup>()
            .HasOne(sig => sig.SubjectGroup)
            .WithMany(sg => sg.SubjectInGroups)
            .HasForeignKey(sig => sig.SubjectGroupId);
        modelBuilder.Entity<SubjectInGroup>()
            .HasOne(sig => sig.Subject)
            .WithMany(s => s.SubjectInGroups)
            .HasForeignKey(sig => sig.SubjectId);

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
            .HasOne(t => t.Department)
            .WithMany(d => d.Teachers)
            .HasForeignKey(t => t.DepartmentId);

        // Fluent API Configuration for TeacherAssignment Entity

        modelBuilder.Entity<TeacherAssignment>(entity =>
        {
            entity.HasKey(ta => ta.Id); // Assuming 'Id' is the primary key from BaseEntity

            // Configure relationships and foreign keys
            entity.HasOne(ta => ta.TeachableSubject)
                .WithMany(t => t.TeacherAssignments)
                .HasForeignKey(ta => ta.TeachableSubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust as necessary

            entity.HasOne(ta => ta.StudentClass)
                .WithMany(sc => sc.TeacherAssignments)
                .HasForeignKey(ta => ta.StudentClassId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust as necessary

            // Configure nullable fields as optional
            entity.Property(ta => ta.TeachableSubjectId)
                .IsRequired(false);

            entity.Property(ta => ta.StudentClassId)
                .IsRequired(false);

            // If you need to configure other properties, add them here
        });


        // TeacherConfig Entity
        modelBuilder.Entity<TeacherConfig>()
            .HasKey(tc => tc.Id);
        modelBuilder.Entity<TeacherConfig>()
            .HasOne(tc => tc.ConfigAttribute)
            .WithMany(ca => ca.TeacherConfigs)
            .HasForeignKey(tc => tc.ConfigAttributeId);
        modelBuilder.Entity<TeacherConfig>()
            .HasOne(tc => tc.Teacher)
            .WithMany(t => t.TeacherConfigs)
            .HasForeignKey(tc => tc.TeacherId);
        modelBuilder.Entity<TeacherConfig>()
            .HasOne(tc => tc.SchoolSchedule)
            .WithMany(ss => ss.TeacherConfigs)
            .HasForeignKey(tc => tc.SchoolScheduleId);

        // TeacherGroup Entity
        modelBuilder.Entity<TeacherGroup>()
            .HasKey(tg => tg.Id);
        modelBuilder.Entity<TeacherGroup>()
            .Property(tg => tg.Name)
            .HasMaxLength(100);
        modelBuilder.Entity<TeacherGroup>()
            .HasOne(tg => tg.School)
            .WithMany(s => s.TeacherGroups)
            .HasForeignKey(tg => tg.SchoolId);

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
        modelBuilder.Entity<Term>()
            .HasOne(t => t.School)
            .WithMany(s => s.Terms)
            .HasForeignKey(t => t.SchoolId);

        // TimeSlot Entity
        modelBuilder.Entity<TimeSlot>()
            .HasKey(ts => ts.Id);
        modelBuilder.Entity<TimeSlot>()
            .Property(ts => ts.Name)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<TimeSlot>()
            .HasOne(ts => ts.School)
            .WithMany(s => s.TimeSlots)
            .HasForeignKey(ts => ts.SchoolId);

    }
}

