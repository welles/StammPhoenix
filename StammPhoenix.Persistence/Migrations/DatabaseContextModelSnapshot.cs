﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StammPhoenix.Persistence;

#nullable disable

namespace StammPhoenix.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StammPhoenix.Persistence.Models.LoginUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_ON");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean")
                        .HasColumnName("IS_LOCKED");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("MODIFIED_ON");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<bool>("NeedPasswordChange")
                        .HasColumnType("boolean")
                        .HasColumnName("NEED_PASSWORD_CHANGE");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PASSWORD_HASH");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("ROLE");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("LOGIN_USER");
                });

            modelBuilder.Entity("StammPhoenix.Persistence.Models.PageContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<string>("AddressCity")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ADDRESS_CITY");

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_ON");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("MODIFIED_ON");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PHONE_NUMBER");

                    b.HasKey("Id");

                    b.ToTable("PAGE_CONTACT");
                });

            modelBuilder.Entity("StammPhoenix.Persistence.Models.PlannedEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CREATED_BY");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_ON");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("END_DATE");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text")
                        .HasColumnName("MODIFIED_BY");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("MODIFIED_ON");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<int>("ParticipatingRanks")
                        .HasColumnType("integer")
                        .HasColumnName("PARTICIPATING_RANKS");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("START_DATE");

                    b.HasKey("Id");

                    b.ToTable("PLANNED_EVENT");
                });
#pragma warning restore 612, 618
        }
    }
}
