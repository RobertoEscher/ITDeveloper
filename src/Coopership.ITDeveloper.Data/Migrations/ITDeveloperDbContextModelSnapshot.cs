﻿// <auto-generated />
using System;
using Coopership.ITDeveloper.Data.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coopership.ITDeveloper.Data.Migrations
{
    [DbContext(typeof(ITDeveloperDbContext))]
    partial class ITDeveloperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Coopership.ITDeveloper.Domain.Entities.Mural", b =>
                {
                    b.Property<int>("MuralId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Autor")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Aviso")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Data");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(100)");

                    b.HasKey("MuralId");

                    b.ToTable("Mural");
                });

            modelBuilder.Entity("Coopership.ITDeveloper.Domain.Models.EstadoPaciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EstadoPaciente");
                });

            modelBuilder.Entity("Coopership.ITDeveloper.Domain.Models.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Cpf")
                        .HasColumnName("Cpf")
                        .HasColumnType("varchar(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11);

                    b.Property<DateTime>("DataInternacao");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("EstadoPacienteId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Rg")
                        .HasColumnName("Rg")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime>("RgDataEmissao");

                    b.Property<string>("RgOrgao")
                        .HasColumnName("RgOrgao")
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Sexo");

                    b.Property<int>("TipoPaciente");

                    b.HasKey("Id");

                    b.HasIndex("EstadoPacienteId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Coopership.ITDeveloper.Domain.Models.Paciente", b =>
                {
                    b.HasOne("Coopership.ITDeveloper.Domain.Models.EstadoPaciente", "EstadoPaciente")
                        .WithMany("Paciente")
                        .HasForeignKey("EstadoPacienteId");
                });
#pragma warning restore 612, 618
        }
    }
}
