using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IGILab1Norm.Migrations
{
    public partial class CarRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BodyNum = table.Column<string>(nullable: true),
                    CarAge = table.Column<int>(nullable: false),
                    CarMark = table.Column<string>(nullable: true),
                    CarPrice = table.Column<int>(nullable: false),
                    DateTO = table.Column<DateTime>(nullable: false),
                    DayPrice = table.Column<int>(nullable: false),
                    EngNum = table.Column<string>(nullable: true),
                    MechFIO = table.Column<string>(nullable: true),
                    Mileage = table.Column<int>(nullable: false),
                    RegNum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adres = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    ClienFIO = table.Column<string>(nullable: true),
                    ClientSex = table.Column<string>(nullable: true),
                    PassNum = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarID = table.Column<int>(nullable: false),
                    ClientID = table.Column<int>(nullable: false),
                    DateGet = table.Column<DateTime>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    RentDate = table.Column<DateTime>(nullable: false),
                    RentPrice = table.Column<int>(nullable: false),
                    WorkerFIO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentID);
                    table.ForeignKey(
                        name: "FK_Rents_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CarID",
                table: "Rents",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ClientID",
                table: "Rents",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
