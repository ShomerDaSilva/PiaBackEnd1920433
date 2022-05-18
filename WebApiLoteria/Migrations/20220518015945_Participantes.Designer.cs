﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiLoteria;

#nullable disable

namespace WebApiLoteria.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220518015945_Participantes")]
    partial class Participantes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiLoteria.Entidades.Participante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApellidoM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RifaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RifaId");

                    b.ToTable("Participantes");
                });

            modelBuilder.Entity("WebApiLoteria.Entidades.Rifa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("NombreRifa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rifas");
                });

            modelBuilder.Entity("WebApiLoteria.Entidades.Participante", b =>
                {
                    b.HasOne("WebApiLoteria.Entidades.Rifa", "Rifa")
                        .WithMany("participantes")
                        .HasForeignKey("RifaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rifa");
                });

            modelBuilder.Entity("WebApiLoteria.Entidades.Rifa", b =>
                {
                    b.Navigation("participantes");
                });
#pragma warning restore 612, 618
        }
    }
}
