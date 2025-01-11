using System;
using HotelUp.Customer.Domain.Consts;
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
                name: "customer");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:document_type", "passport,id_card")
                .Annotation("Npgsql:Enum:presence_status", "pending,checked_in,checked_out")
                .Annotation("Npgsql:Enum:reservation_status", "valid,canceled")
                .Annotation("Npgsql:Enum:room_type", "economy,basic,premium");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "customer",
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
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    WithSpecialNeeds = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<RoomType>(type: "customer.room_type", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<ReservationStatus>(type: "customer.reservation_status", nullable: false),
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
                        principalSchema: "customer",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "customer",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccomodationPrice_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AccomodationPrice_Currency = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ReservationId);
                    table.UniqueConstraint("AK_Bills_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "customer",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRoom",
                schema: "customer",
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
                        principalSchema: "customer",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalSchema: "customer",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Pesel = table.Column<string>(type: "text", nullable: false),
                    DocumentType = table.Column<DocumentType>(type: "customer.document_type", nullable: false),
                    Status = table.Column<PresenceStatus>(type: "customer.presence_status", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => new { x.ReservationId, x.Id });
                    table.ForeignKey(
                        name: "FK_Tenants_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "customer",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalCost",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price_Currency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalCost", x => new { x.BillId, x.Id });
                    table.ForeignKey(
                        name: "FK_AdditionalCost_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "customer",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_Currency = table.Column<string>(type: "text", nullable: false),
                    SettlementDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => new { x.BillId, x.Id });
                    table.ForeignKey(
                        name: "FK_Payment_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "customer",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRoom_RoomsId",
                schema: "customer",
                table: "ReservationRoom",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                schema: "customer",
                table: "Reservations",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalCost",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "ReservationRoom",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "customer");
        }
    }
}
