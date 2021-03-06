﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace senai.spacekids.repository.Migrations
{
    public partial class spacekids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    nome = table.Column<string>(maxLength: 100, nullable: false),
                    senha = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Criancas",
                columns: table => new
                {
                    CriancaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    foto = table.Column<string>(nullable: true),
                    idade = table.Column<int>(nullable: false),
                    loginId = table.Column<int>(nullable: false),
                    nome = table.Column<string>(maxLength: 100, nullable: false),
                    sexo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criancas", x => x.CriancaId);
                    table.ForeignKey(
                        name: "FK_Criancas_Logins_loginId",
                        column: x => x.loginId,
                        principalTable: "Logins",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fases",
                columns: table => new
                {
                    FaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriancaId = table.Column<int>(nullable: true),
                    nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fases", x => x.FaseId);
                    table.ForeignKey(
                        name: "FK_Fases_Criancas_CriancaId",
                        column: x => x.CriancaId,
                        principalTable: "Criancas",
                        principalColumn: "CriancaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Desempenhos",
                columns: table => new
                {
                    desempenhoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    acertou = table.Column<bool>(nullable: false),
                    criancaId = table.Column<int>(nullable: false),
                    faseId = table.Column<int>(nullable: false),
                    horaFinal = table.Column<DateTime>(nullable: false),
                    horaInicial = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desempenhos", x => x.desempenhoId);
                    table.ForeignKey(
                        name: "FK_Desempenhos_Criancas_criancaId",
                        column: x => x.criancaId,
                        principalTable: "Criancas",
                        principalColumn: "CriancaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Desempenhos_Fases_faseId",
                        column: x => x.faseId,
                        principalTable: "Fases",
                        principalColumn: "FaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criancas_loginId",
                table: "Criancas",
                column: "loginId");

            migrationBuilder.CreateIndex(
                name: "IX_Desempenhos_criancaId",
                table: "Desempenhos",
                column: "criancaId");

            migrationBuilder.CreateIndex(
                name: "IX_Desempenhos_faseId",
                table: "Desempenhos",
                column: "faseId");

            migrationBuilder.CreateIndex(
                name: "IX_Fases_CriancaId",
                table: "Fases",
                column: "CriancaId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_email",
                table: "Logins",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desempenhos");

            migrationBuilder.DropTable(
                name: "Fases");

            migrationBuilder.DropTable(
                name: "Criancas");

            migrationBuilder.DropTable(
                name: "Logins");
        }
    }
}
