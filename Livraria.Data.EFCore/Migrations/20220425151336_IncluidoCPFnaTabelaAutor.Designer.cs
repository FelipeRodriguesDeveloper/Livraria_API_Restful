﻿// <auto-generated />
using System;
using Livraria.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LivrariaAPI.Migrations
{
    [DbContext(typeof(LivrariaDBContext))]
    [Migration("20220425151336_IncluidoCPFnaTabelaAutor")]
    partial class IncluidoCPFnaTabelaAutor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Livraria.Domain.Entidades.Autor", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("aut_codigo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("aut_cpf");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("aut_dtnascimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("aut_nome");

                    b.HasKey("Codigo");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("Livraria.Domain.Entidades.Livro", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("liv_codigo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("liv_datacadastro");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("liv_preco");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("liv_titulo");

                    b.HasKey("Codigo");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("Livraria.Domain.Entidades.LivroAutor", b =>
                {
                    b.Property<int>("CodigoLivro")
                        .HasColumnType("int")
                        .HasColumnName("liv_codigo");

                    b.Property<int>("CodigoAutor")
                        .HasColumnType("int")
                        .HasColumnName("aut_codigo");

                    b.HasKey("CodigoLivro", "CodigoAutor");

                    b.HasIndex("CodigoAutor");

                    b.ToTable("Livros_autores");
                });

            modelBuilder.Entity("Livraria.Domain.Entidades.LivroAutor", b =>
                {
                    b.HasOne("Livraria.Domain.Entidades.Autor", "Autor")
                        .WithMany("LivrosAutores")
                        .HasForeignKey("CodigoAutor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Livraria.Domain.Entidades.Livro", "Livro")
                        .WithMany("LivrosAutores")
                        .HasForeignKey("CodigoLivro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("Livraria.Domain.Entidades.Autor", b =>
                {
                    b.Navigation("LivrosAutores");
                });

            modelBuilder.Entity("Livraria.Domain.Entidades.Livro", b =>
                {
                    b.Navigation("LivrosAutores");
                });
#pragma warning restore 612, 618
        }
    }
}
