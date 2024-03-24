using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Asset.Booking.Infrastructure.Migrations.Booking
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.CreateTable(
                name: "asset_schedules",
                schema: "booking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    asset_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_schedules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "booking",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    adr_city = table.Column<string>(type: "text", nullable: true),
                    adr_zip = table.Column<string>(type: "text", nullable: true),
                    adr_street = table.Column<string>(type: "text", nullable: true),
                    adr_street_nr = table.Column<string>(type: "text", nullable: true),
                    company_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phone_numbers",
                schema: "booking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phone_numbers", x => x.id);
                    table.ForeignKey(
                        name: "FK_phone_numbers_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "booking",
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                schema: "booking",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    schedule_id = table.Column<int>(type: "integer", nullable: false),
                    moderator_id = table.Column<int>(type: "integer", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    interval_start = table.Column<DateTime>(type: "Date", nullable: false),
                    interval_end = table.Column<DateTime>(type: "Date", nullable: false),
                    price_per_person = table.Column<decimal>(type: "numeric", nullable: false),
                    service_fee = table.Column<decimal>(type: "numeric", nullable: false),
                    nr_of_people = table.Column<int>(type: "integer", nullable: false),
                    nr_of_nights = table.Column<int>(type: "integer", nullable: false),
                    vat_percent = table.Column<float>(type: "real", nullable: false),
                    vat_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    total_cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_asset_schedules_schedule_id",
                        column: x => x.schedule_id,
                        principalSchema: "booking",
                        principalTable: "asset_schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "booking",
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_phone_numbers_client_id",
                schema: "booking",
                table: "phone_numbers",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_client_id",
                schema: "booking",
                table: "reservations",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_schedule_id",
                schema: "booking",
                table: "reservations",
                column: "schedule_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "phone_numbers",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "reservations",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "asset_schedules",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "clients",
                schema: "booking");
        }
    }
}
