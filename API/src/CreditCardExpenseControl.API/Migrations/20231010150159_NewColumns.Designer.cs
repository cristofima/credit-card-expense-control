﻿// <auto-generated />
using System;
using CreditCardExpenseControl.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CreditCardExpenseControl.API.Migrations
{
    [DbContext(typeof(CreditCardExpenseControlContext))]
    [Migration("20231010150159_NewColumns")]
    partial class NewColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CreditCardExpenseControl.API.Entities.CreditCard", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CutOffDay")
                        .HasColumnType("int");

                    b.Property<double>("DeferredContributionPercentage")
                        .HasColumnType("float");

                    b.Property<int>("ExpirationMonth")
                        .HasColumnType("int");

                    b.Property<int>("ExpirationYear")
                        .HasColumnType("int");

                    b.Property<string>("Last4Digits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("CreditCardExpenseControl.API.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CreditCardId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GraceMonths")
                        .HasColumnType("int");

                    b.Property<int>("Quotas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CreditCardExpenseControl.API.Entities.Transaction", b =>
                {
                    b.HasOne("CreditCardExpenseControl.API.Entities.CreditCard", "CreditCard")
                        .WithMany("Transactions")
                        .HasForeignKey("CreditCardId");

                    b.Navigation("CreditCard");
                });

            modelBuilder.Entity("CreditCardExpenseControl.API.Entities.CreditCard", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
