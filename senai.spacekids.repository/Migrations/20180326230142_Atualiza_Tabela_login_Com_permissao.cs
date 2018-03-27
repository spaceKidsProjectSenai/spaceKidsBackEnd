using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace senai.spacekids.repository.Migrations
{
    public partial class Atualiza_Tabela_login_Com_permissao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permissao",
                table: "Logins",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissao",
                table: "Logins");
        }
    }
}
