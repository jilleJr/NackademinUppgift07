﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using NackademinUppgift07.Models;
using System;

namespace NackademinUppgift07.Migrations
{
    [DbContext(typeof(TomasosContext))]
    partial class TomasosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NackademinUppgift07.Models.Bestallning", b =>
                {
                    b.Property<int>("BestallningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BestallningID");

                    b.Property<DateTime>("BestallningDatum")
                        .HasColumnType("datetime");

                    b.Property<int>("KundId")
                        .HasColumnName("KundID");

                    b.Property<bool>("Levererad");

                    b.Property<int>("Totalbelopp");

                    b.HasKey("BestallningId");

                    b.HasIndex("KundId");

                    b.ToTable("Bestallning");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.BestallningMatratt", b =>
                {
                    b.Property<int>("MatrattId")
                        .HasColumnName("MatrattID");

                    b.Property<int>("BestallningId")
                        .HasColumnName("BestallningID");

                    b.Property<int>("Antal")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((1))");

                    b.HasKey("MatrattId", "BestallningId");

                    b.HasIndex("BestallningId");

                    b.ToTable("BestallningMatratt");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.Kund", b =>
                {
                    b.Property<int>("KundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("KundID");

                    b.Property<string>("AnvandarNamn")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Gatuadress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Losenord")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Postnr")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Postort")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Telefon")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("KundId");

                    b.ToTable("Kund");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.Matratt", b =>
                {
                    b.Property<int>("MatrattId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MatrattID");

                    b.Property<string>("Beskrivning")
                        .HasMaxLength(200)
                        .IsUnicode(false);

                    b.Property<string>("MatrattNamn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("MatrattTyp");

                    b.Property<int>("Pris");

                    b.HasKey("MatrattId");

                    b.HasIndex("MatrattTyp");

                    b.ToTable("Matratt");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.MatrattProdukt", b =>
                {
                    b.Property<int>("MatrattId")
                        .HasColumnName("MatrattID");

                    b.Property<int>("ProduktId")
                        .HasColumnName("ProduktID");

                    b.HasKey("MatrattId", "ProduktId");

                    b.HasIndex("ProduktId");

                    b.ToTable("MatrattProdukt");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.MatrattTyp", b =>
                {
                    b.Property<int>("MatrattTyp1")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MatrattTyp");

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("MatrattTyp1");

                    b.ToTable("MatrattTyp");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.Produkt", b =>
                {
                    b.Property<int>("ProduktId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProduktID");

                    b.Property<string>("ProduktNamn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("ProduktId");

                    b.ToTable("Produkt");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.Bestallning", b =>
                {
                    b.HasOne("NackademinUppgift07.Models.Kund", "Kund")
                        .WithMany("Bestallning")
                        .HasForeignKey("KundId")
                        .HasConstraintName("FK_Bestallning_Kund");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.BestallningMatratt", b =>
                {
                    b.HasOne("NackademinUppgift07.Models.Bestallning", "Bestallning")
                        .WithMany("BestallningMatratt")
                        .HasForeignKey("BestallningId")
                        .HasConstraintName("FK_BestallningMatratt_Bestallning");

                    b.HasOne("NackademinUppgift07.Models.Matratt", "Matratt")
                        .WithMany("BestallningMatratt")
                        .HasForeignKey("MatrattId")
                        .HasConstraintName("FK_BestallningMatratt_Matratt");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.Matratt", b =>
                {
                    b.HasOne("NackademinUppgift07.Models.MatrattTyp", "MatrattTypNavigation")
                        .WithMany("Matratt")
                        .HasForeignKey("MatrattTyp")
                        .HasConstraintName("FK_Matratt_MatrattTyp");
                });

            modelBuilder.Entity("NackademinUppgift07.Models.MatrattProdukt", b =>
                {
                    b.HasOne("NackademinUppgift07.Models.Matratt", "Matratt")
                        .WithMany("MatrattProdukt")
                        .HasForeignKey("MatrattId")
                        .HasConstraintName("FK_MatrattProdukt_Matratt");

                    b.HasOne("NackademinUppgift07.Models.Produkt", "Produkt")
                        .WithMany("MatrattProdukt")
                        .HasForeignKey("ProduktId")
                        .HasConstraintName("FK_MatrattProdukt_Produkt");
                });
#pragma warning restore 612, 618
        }
    }
}
