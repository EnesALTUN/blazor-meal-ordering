using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealOrdering.Server.Data.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "varchar", maxLength: 250, nullable: false),
                    web_url = table.Column<string>(type: "varchar", maxLength: 2048, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    first_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    email_address = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "varchar", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: false),
                    expire_date = table.Column<DateTime>(type: "date", nullable: false),
                    created_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_user_id",
                        column: x => x.created_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    created_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()"),
                    modified_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sub_order_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_sub_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sub_order_user_id",
                        column: x => x.created_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_created_user_id",
                table: "order",
                column: "created_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_id",
                table: "order",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_order_supplier_id",
                table: "order",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_order_created_user_id",
                table: "sub_order",
                column: "created_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_order_id",
                table: "sub_order",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_order_order_id",
                table: "sub_order",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_id",
                table: "supplier",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_id",
                table: "user",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sub_order");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
