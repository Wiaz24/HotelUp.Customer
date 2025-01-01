using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelUp.Customer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HotelUp.Customer");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    WithSpecialNeeds = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Period_From = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Period_To = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccomodationPrice_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AccomodationPrice_Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Reservations_Id",
                        column: x => x.Id,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRoom",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRoom", x => new { x.ReservationId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_ReservationRoom_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Pesel = table.Column<string>(type: "text", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalCosts",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalCosts_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "HotelUp.Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "HotelUp.Customer",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalCosts_BillId",
                schema: "HotelUp.Customer",
                table: "AdditionalCosts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                schema: "HotelUp.Customer",
                table: "Payments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRoom_RoomsId",
                schema: "HotelUp.Customer",
                table: "ReservationRoom",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                schema: "HotelUp.Customer",
                table: "Reservations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ReservationId",
                schema: "HotelUp.Customer",
                table: "Tenants",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalCosts",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "ReservationRoom",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "HotelUp.Customer");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "HotelUp.Customer");
        }
    }
}
