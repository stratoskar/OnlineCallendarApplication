﻿// <auto-generated />
using System;
using Callendar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Callendar.Data.Migrations
{
    [DbContext(typeof(CallendarDataContext))]
    [Migration("20220917101817_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            modelBuilder.Entity("Callendar.Data.Event", b =>
                {
                    b.Property<int>("Event_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string[]>("Collaborators")
                        .HasColumnType("text[]");

                    b.Property<DateTime>("Date_Hour")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Owner_Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Event_ID");

                    b.HasIndex("Owner_Username");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Callendar.Data.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Callendar.Data.Event", b =>
                {
                    b.HasOne("Callendar.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("Owner_Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}