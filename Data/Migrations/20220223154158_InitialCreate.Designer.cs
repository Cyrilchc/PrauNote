﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220223154158_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AppointmentName")
                        .HasColumnType("longtext");

                    b.Property<int?>("AppointmentStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("AppointmentId");

                    b.HasIndex("AppointmentStatusId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Models.AppointmentStatus", b =>
                {
                    b.Property<int>("AppointmentStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AppointmentStatusDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("AppointmentStatusName")
                        .HasColumnType("longtext");

                    b.HasKey("AppointmentStatusId");

                    b.ToTable("AppointmentStatuses");
                });

            modelBuilder.Entity("Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AssignmentName")
                        .HasColumnType("longtext");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("AssignmentId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Models.Attachment", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.HasKey("AttachmentId");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Models.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("AssignmentGrade")
                        .HasColumnType("double");

                    b.Property<int?>("AssignmentId")
                        .HasColumnType("int");

                    b.HasKey("GradeId");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .HasColumnType("longtext");

                    b.HasKey("GroupId");

                    b.HasIndex("AppointmentId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GivenName")
                        .HasColumnType("longtext");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.HasKey("StudentId");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("longtext");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Models.Appointment", b =>
                {
                    b.HasOne("Models.AppointmentStatus", "AppointmentStatus")
                        .WithMany()
                        .HasForeignKey("AppointmentStatusId");

                    b.Navigation("AppointmentStatus");
                });

            modelBuilder.Entity("Models.Assignment", b =>
                {
                    b.HasOne("Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Models.Attachment", b =>
                {
                    b.HasOne("Models.Assignment", null)
                        .WithMany("Attachments")
                        .HasForeignKey("AssignmentId");
                });

            modelBuilder.Entity("Models.Grade", b =>
                {
                    b.HasOne("Models.Assignment", "Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId");

                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("Models.Group", b =>
                {
                    b.HasOne("Models.Appointment", null)
                        .WithMany("Groups")
                        .HasForeignKey("AppointmentId");
                });

            modelBuilder.Entity("Models.Student", b =>
                {
                    b.HasOne("Models.Group", null)
                        .WithMany("Students")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Models.Appointment", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Models.Assignment", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Models.Group", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
