﻿// <auto-generated />
using System;
using EntityFrameWork.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFrameWork.Migrations
{
    [DbContext(typeof(PersonDbContext))]
    [Migration("20241224043006_AddPersonTIN")]
    partial class AddPersonTIN
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EntityFrameWork.Entities.Country", b =>
                {
                    b.Property<Guid>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryId = new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"),
                            CountryName = "United States"
                        },
                        new
                        {
                            CountryId = new Guid("78e912ac-d73d-463b-9f39-37e53ef9e8b7"),
                            CountryName = "Canada"
                        },
                        new
                        {
                            CountryId = new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"),
                            CountryName = "United Kingdom"
                        },
                        new
                        {
                            CountryId = new Guid("b78c0f21-3179-4976-9bbf-49d5a37b234e"),
                            CountryName = "Australia"
                        });
                });

            modelBuilder.Entity("EntityFrameWork.Entities.Person", b =>
                {
                    b.Property<Guid>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PersonName")
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<bool>("ReceiveNewsLetter")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TIN")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(8)")
                        .HasDefaultValue("ehehe")
                        .HasColumnName("TaxIDnotation");

                    b.HasKey("PersonId");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("d75f8a62-bfb5-4c45-a5b3-6ad0e88a2c0a"),
                            Address = "123 Elm Street, Springfield",
                            CountryId = new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"),
                            DateOfBirth = new DateTime(1985, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johndoe@example.com",
                            Gender = "Male",
                            PersonName = "John Doe",
                            ReceiveNewsLetter = true
                        },
                        new
                        {
                            PersonId = new Guid("b234f654-a8b2-42b6-9f4a-90d58ebc3fcb"),
                            Address = "456 Oak Avenue, Metropolis",
                            CountryId = new Guid("78e912ac-d73d-463b-9f39-37e53ef9e8b7"),
                            DateOfBirth = new DateTime(1990, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "janesmith@example.com",
                            Gender = "Female",
                            PersonName = "Jane Smith",
                            ReceiveNewsLetter = false
                        },
                        new
                        {
                            PersonId = new Guid("d912f360-e1a4-48fc-9461-fae8c22960f3"),
                            Address = "789 Maple Lane, Gotham",
                            CountryId = new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"),
                            DateOfBirth = new DateTime(1978, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "samlee@example.com",
                            Gender = "Male",
                            PersonName = "Samuel Lee",
                            ReceiveNewsLetter = true
                        },
                        new
                        {
                            PersonId = new Guid("f456789c-435a-4e4b-975c-d416ad8d35db"),
                            Address = "321 Birch Boulevard, Star City",
                            CountryId = new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"),
                            DateOfBirth = new DateTime(2000, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "emilydavis@example.com",
                            Gender = "Female",
                            PersonName = "Emily Davis",
                            ReceiveNewsLetter = false
                        },
                        new
                        {
                            PersonId = new Guid("e412c1c6-8823-44fc-a4bd-8e0df1fcaa9c"),
                            Address = "654 Pine Street, Central City",
                            CountryId = new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"),
                            DateOfBirth = new DateTime(1995, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "michaelbrown@example.com",
                            Gender = "Male",
                            PersonName = "Michael Brown",
                            ReceiveNewsLetter = true
                        },
                        new
                        {
                            PersonId = new Guid("c49cce9a-d6a3-4fbb-b1cf-5e9a84b92c4f"),
                            Address = "987 Cedar Court, Coast City",
                            CountryId = new Guid("b78c0f21-3179-4976-9bbf-49d5a37b234e"),
                            DateOfBirth = new DateTime(1982, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sophiamartinez@example.com",
                            Gender = "Female",
                            PersonName = "Sophia Martinez",
                            ReceiveNewsLetter = false
                        },
                        new
                        {
                            PersonId = new Guid("d2e4f962-2e17-4a09-9d90-e29f2d9158b7"),
                            Address = "147 Spruce Drive, Riverdale",
                            CountryId = new Guid("78e912ac-d73d-463b-9f39-37e53ef9e8b7"),
                            DateOfBirth = new DateTime(1970, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "williamtaylor@example.com",
                            Gender = "Male",
                            PersonName = "William Taylor",
                            ReceiveNewsLetter = true
                        },
                        new
                        {
                            PersonId = new Guid("f3d74215-57a6-4f02-85e7-9c4e3f4b7c30"),
                            Address = "852 Willow Street, Hill Valley",
                            CountryId = new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"),
                            DateOfBirth = new DateTime(1998, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "isabellajohnson@example.com",
                            Gender = "Female",
                            PersonName = "Isabella Johnson",
                            ReceiveNewsLetter = true
                        },
                        new
                        {
                            PersonId = new Guid("a3d91284-dfc9-437e-9a7d-204f9fc037da"),
                            Address = "159 Aspen Way, Emerald City",
                            CountryId = new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"),
                            DateOfBirth = new DateTime(1965, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "oliverwilson@example.com",
                            Gender = "Male",
                            PersonName = "Oliver Wilson",
                            ReceiveNewsLetter = false
                        },
                        new
                        {
                            PersonId = new Guid("f3d02915-b7a9-4f27-b8c6-a937c4c287c6"),
                            Address = "963 Redwood Road, Beacon Hills",
                            CountryId = new Guid("b78c0f21-3179-4976-9bbf-49d5a37b234e"),
                            DateOfBirth = new DateTime(1992, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "charlottemoore@example.com",
                            Gender = "Female",
                            PersonName = "Charlotte Moore",
                            ReceiveNewsLetter = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
