using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KRCP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RUC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactoEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "ERP_IntegracionLog",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntidadAfectada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntidadID = table.Column<int>(type: "int", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayloadJSON = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERP_IntegracionLog", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    UbicacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoUbicacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.UbicacionID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolID",
                        column: x => x.RolID,
                        principalTable: "Roles",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesTrabajo",
                columns: table => new
                {
                    OTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoOT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    EquipoComponente = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsuarioPlanificadorID = table.Column<int>(type: "int", nullable: false),
                    TecnicoAsignadoID = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Registrado"),
                    FechaIngreso = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaEstimadaEntrega = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaCierre = table.Column<DateTime>(type: "datetime", nullable: true),
                    CostoManoObra = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    CostoRepuestos = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    Observaciones = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesTrabajo", x => x.OTID);
                    table.ForeignKey(
                        name: "FK_OrdenesTrabajo_Clientes_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Clientes",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesTrabajo_Usuarios_TecnicoAsignadoID",
                        column: x => x.TecnicoAsignadoID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrdenesTrabajo_Usuarios_UsuarioModificacionID",
                        column: x => x.UsuarioModificacionID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesTrabajo_Usuarios_UsuarioPlanificadorID",
                        column: x => x.UsuarioPlanificadorID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoParte = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StockActual = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    StockMinimo = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    CostoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    CategoriaID = table.Column<int>(type: "int", nullable: false),
                    UbicacionID = table.Column<int>(type: "int", nullable: true),
                    UsuarioModificacionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoID);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Ubicaciones_UbicacionID",
                        column: x => x.UbicacionID,
                        principalTable: "Ubicaciones",
                        principalColumn: "UbicacionID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Productos_Usuarios_UsuarioModificacionID",
                        column: x => x.UsuarioModificacionID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OT_Evidencias",
                columns: table => new
                {
                    EvidenciaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OTID = table.Column<int>(type: "int", nullable: false),
                    TipoEvidencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UsuarioCargaID = table.Column<int>(type: "int", nullable: false),
                    FechaCarga = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_Evidencias", x => x.EvidenciaID);
                    table.ForeignKey(
                        name: "FK_OT_Evidencias_OrdenesTrabajo_OTID",
                        column: x => x.OTID,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "OTID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_Evidencias_Usuarios_UsuarioCargaID",
                        column: x => x.UsuarioCargaID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosKardex",
                columns: table => new
                {
                    KardexID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    OTID = table.Column<int>(type: "int", nullable: true),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    StockAnterior = table.Column<int>(type: "int", nullable: false),
                    StockNuevo = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosKardex", x => x.KardexID);
                    table.ForeignKey(
                        name: "FK_MovimientosKardex_OrdenesTrabajo_OTID",
                        column: x => x.OTID,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "OTID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MovimientosKardex_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimientosKardex_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OT_RepuestosConsumidos",
                columns: table => new
                {
                    DetalleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OTID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitarioHistorico = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    UsuarioAlmacenID = table.Column<int>(type: "int", nullable: false),
                    FechaDespacho = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OT_RepuestosConsumidos", x => x.DetalleID);
                    table.ForeignKey(
                        name: "FK_OT_RepuestosConsumidos_OrdenesTrabajo_OTID",
                        column: x => x.OTID,
                        principalTable: "OrdenesTrabajo",
                        principalColumn: "OTID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_RepuestosConsumidos_Productos_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OT_RepuestosConsumidos_Usuarios_UsuarioAlmacenID",
                        column: x => x.UsuarioAlmacenID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_RUC",
                table: "Clientes",
                column: "RUC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosKardex_OTID",
                table: "MovimientosKardex",
                column: "OTID");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosKardex_ProductoID",
                table: "MovimientosKardex",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosKardex_UsuarioID",
                table: "MovimientosKardex",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTrabajo_ClienteID",
                table: "OrdenesTrabajo",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTrabajo_CodigoOT",
                table: "OrdenesTrabajo",
                column: "CodigoOT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTrabajo_TecnicoAsignadoID",
                table: "OrdenesTrabajo",
                column: "TecnicoAsignadoID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTrabajo_UsuarioModificacionID",
                table: "OrdenesTrabajo",
                column: "UsuarioModificacionID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTrabajo_UsuarioPlanificadorID",
                table: "OrdenesTrabajo",
                column: "UsuarioPlanificadorID");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Evidencias_OTID",
                table: "OT_Evidencias",
                column: "OTID");

            migrationBuilder.CreateIndex(
                name: "IX_OT_Evidencias_UsuarioCargaID",
                table: "OT_Evidencias",
                column: "UsuarioCargaID");

            migrationBuilder.CreateIndex(
                name: "IX_OT_RepuestosConsumidos_OTID",
                table: "OT_RepuestosConsumidos",
                column: "OTID");

            migrationBuilder.CreateIndex(
                name: "IX_OT_RepuestosConsumidos_ProductoID",
                table: "OT_RepuestosConsumidos",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_OT_RepuestosConsumidos_UsuarioAlmacenID",
                table: "OT_RepuestosConsumidos",
                column: "UsuarioAlmacenID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaID",
                table: "Productos",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoParte",
                table: "Productos",
                column: "CodigoParte",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UbicacionID",
                table: "Productos",
                column: "UbicacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UsuarioModificacionID",
                table: "Productos",
                column: "UsuarioModificacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NombreRol",
                table: "Roles",
                column: "NombreRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_CodigoUbicacion",
                table: "Ubicaciones",
                column: "CodigoUbicacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolID",
                table: "Usuarios",
                column: "RolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ERP_IntegracionLog");

            migrationBuilder.DropTable(
                name: "MovimientosKardex");

            migrationBuilder.DropTable(
                name: "OT_Evidencias");

            migrationBuilder.DropTable(
                name: "OT_RepuestosConsumidos");

            migrationBuilder.DropTable(
                name: "OrdenesTrabajo");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
