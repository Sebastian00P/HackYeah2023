﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackYeahGWIZDapi.Migrations
{
    public partial class fixDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Events",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredTime",
                table: "Events",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ExpiredTime",
                table: "Events");
        }
    }
}
