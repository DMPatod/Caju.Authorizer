﻿// <auto-generated />
using System;
using Caju.Authorizer.Infrastructure.DataPersistence.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Caju.Authorizer.Infrastructure.DataPersistence.Migrations
{
    [DbContext(typeof(SQLServerContext))]
    [Migration("20240910015616_CreateAuthorizerTables")]
    partial class CreateAuthorizerTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Caju.Authorizer.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AmountCash")
                        .HasColumnType("float");

                    b.Property<double>("AmountFood")
                        .HasColumnType("float");

                    b.Property<double>("AmountMeal")
                        .HasColumnType("float");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("Caju.Authorizer.Domain.Transactions.Entities.TransactionIntent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Authorized")
                        .HasColumnType("bit");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetaData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionIntents", (string)null);
                });

            modelBuilder.Entity("Caju.Authorizer.Domain.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("MCC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Merchant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("Caju.Authorizer.Domain.Transactions.Entities.TransactionIntent", b =>
                {
                    b.HasOne("Caju.Authorizer.Domain.Transactions.Transaction", null)
                        .WithMany("TransactionIntents")
                        .HasForeignKey("TransactionId");
                });

            modelBuilder.Entity("Caju.Authorizer.Domain.Transactions.Transaction", b =>
                {
                    b.Navigation("TransactionIntents");
                });
#pragma warning restore 612, 618
        }
    }
}
