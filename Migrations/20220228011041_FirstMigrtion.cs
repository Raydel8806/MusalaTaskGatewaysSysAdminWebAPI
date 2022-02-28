﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatewaysSysAdminWebAPI.Migrations
{
    public partial class FirstMigrtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateway",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxClientNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateway", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeripheralDevice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceVendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtDeviceCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    GatewayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeripheralDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeripheralDevice_Gateway_GatewayId",
                        column: x => x.GatewayId,
                        principalTable: "Gateway",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeripheralDevice_GatewayId",
                table: "PeripheralDevice",
                column: "GatewayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeripheralDevice");

            migrationBuilder.DropTable(
                name: "Gateway");
        }
    }
}
