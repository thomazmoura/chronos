﻿// <auto-generated />
using Chronos.API.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Chronos.API.Dados.Migrations
{
    [DbContext(typeof(ContextoDeDadosChronos))]
    [Migration("20180224230110_RemocaoDeAcentos")]
    partial class RemocaoDeAcentos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Chronos.API.Entidades.Contrato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ValorDaHora");

                    b.HasKey("Id");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("Chronos.API.Entidades.Folha", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ContratoId");

                    b.Property<DateTime?>("DataFinal");

                    b.Property<DateTime>("DataInicial");

                    b.Property<int>("QuantidadePrevistaDeDiasÚteis");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("Folhas");
                });

            modelBuilder.Entity("Chronos.API.Entidades.Periodo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.Property<Guid>("FolhaId");

                    b.Property<DateTime>("HorarioDeEncerramento");

                    b.Property<DateTime>("HorarioDeInicio");

                    b.HasKey("Id");

                    b.HasIndex("FolhaId");

                    b.ToTable("Periodos");
                });

            modelBuilder.Entity("Chronos.API.Entidades.Folha", b =>
                {
                    b.HasOne("Chronos.API.Entidades.Contrato")
                        .WithMany("Folhas")
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chronos.API.Entidades.Periodo", b =>
                {
                    b.HasOne("Chronos.API.Entidades.Folha")
                        .WithMany("Periodos")
                        .HasForeignKey("FolhaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}