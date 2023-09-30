﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackYeahGWIZDapi.Migrations
{
    public partial class expiredTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredTime",
                table: "Events");
        }
    }
}
