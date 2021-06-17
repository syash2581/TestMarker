﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OTM.Data;

namespace OTM.Migrations
{
    [DbContext(typeof(OTMContext))]
    [Migration("20201014101109_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OTM.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeptSname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Deptcode")
                        .HasColumnType("int");

                    b.Property<string>("Deptname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OTM.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("mno")
                        .HasColumnType("float");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("regornot")
                        .HasColumnType("int");

                    b.Property<string>("rollno")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("rollno")
                        .IsUnique()
                        .HasFilter("[rollno] IS NOT NULL");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("OTM.Models.Options", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Correct")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("OTM.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Marks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionBrief")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<string>("question")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("OTM.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dept")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Mno")
                        .HasColumnType("float");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sem")
                        .HasColumnType("int");

                    b.Property<string>("Sname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("rollno")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("rollno")
                        .IsUnique()
                        .HasFilter("[rollno] IS NOT NULL");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("OTM.Models.StudentTest", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<DateTime>("attendedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("results")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "TestId");

                    b.HasIndex("TestId");

                    b.ToTable("StudentTests");
                });

            modelBuilder.Entity("OTM.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int>("Forsem")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TestEndtime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Testdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Testduration")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalQuestions")
                        .HasColumnType("int");

                    b.Property<int>("Totalmarks")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("OTM.Models.Faculty", b =>
                {
                    b.HasOne("OTM.Models.Department", "department")
                        .WithMany("Faculty")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OTM.Models.Options", b =>
                {
                    b.HasOne("OTM.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OTM.Models.Question", b =>
                {
                    b.HasOne("OTM.Models.Test", "Test")
                        .WithMany("questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OTM.Models.StudentTest", b =>
                {
                    b.HasOne("OTM.Models.Student", "student")
                        .WithMany("studentTests")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OTM.Models.Test", "test")
                        .WithMany("studentTests")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OTM.Models.Test", b =>
                {
                    b.HasOne("OTM.Models.Faculty", "Faculty")
                        .WithMany("tests")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}