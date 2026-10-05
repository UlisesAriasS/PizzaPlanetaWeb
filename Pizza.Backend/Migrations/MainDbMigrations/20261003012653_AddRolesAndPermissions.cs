using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pizza.Backend.Migrations.MainDbMigrations
{
    /// <inheritdoc />
    public partial class AddRolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_sucursales_SucursalId",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_SucursalId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "usuarios");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SucursaleId",
                table: "Pedido",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistorialAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    FechaAcceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    DireccionIp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAccesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialAccesos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    PermisoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => new { x.RolId, x.PermisoId });
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_RolId",
                table: "usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_SucursaleId",
                table: "Pedido",
                column: "SucursaleId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAccesos_UsuarioId",
                table: "HistorialAccesos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoId",
                table: "RolPermisos",
                column: "PermisoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_sucursales_SucursaleId",
                table: "Pedido",
                column: "SucursaleId",
                principalTable: "sucursales",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_Roles_RolId",
                table: "usuarios",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_sucursales_SucursaleId",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_Roles_RolId",
                table: "usuarios");

            migrationBuilder.DropTable(
                name: "HistorialAccesos");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_usuarios_RolId",
                table: "usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_SucursaleId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "SucursaleId",
                table: "Pedido");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_SucursalId",
                table: "Pedido",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_sucursales_SucursalId",
                table: "Pedido",
                column: "SucursalId",
                principalTable: "sucursales",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
