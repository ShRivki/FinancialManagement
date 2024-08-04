﻿// <auto-generated />
using System;
using FinancialManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinancialManagement.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240801103849_add fild repaymentDate")]
    partial class addfildrepaymentDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FinancialManagement.Core.Entities.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateOfMaturity")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepositorId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DepositorId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DonorId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Guarantee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GuarantorId")
                        .HasColumnType("int");

                    b.Property<int>("LoanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuarantorId");

                    b.HasIndex("LoanId");

                    b.ToTable("Guarantees");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPayment")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RepaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TotalPayments")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Deposit", b =>
                {
                    b.HasOne("FinancialManagement.Core.Entities.User", "Depositor")
                        .WithMany("Deposits")
                        .HasForeignKey("DepositorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Depositor");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Donation", b =>
                {
                    b.HasOne("FinancialManagement.Core.Entities.User", "Donor")
                        .WithMany("Donations")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Guarantee", b =>
                {
                    b.HasOne("FinancialManagement.Core.Entities.User", "Guarantor")
                        .WithMany("Guarantees")
                        .HasForeignKey("GuarantorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialManagement.Core.Entities.Loan", "Loan")
                        .WithMany("Guarantees")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guarantor");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Loan", b =>
                {
                    b.HasOne("FinancialManagement.Core.Entities.User", "Borrower")
                        .WithMany("Loans")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.Loan", b =>
                {
                    b.Navigation("Guarantees");
                });

            modelBuilder.Entity("FinancialManagement.Core.Entities.User", b =>
                {
                    b.Navigation("Deposits");

                    b.Navigation("Donations");

                    b.Navigation("Guarantees");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
