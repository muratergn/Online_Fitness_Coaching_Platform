using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineFitnessCoachingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kullaniciAntrenor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DanisanId = table.Column<int>(type: "int", nullable: false),
                    AntrenorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullaniciAntrenor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilFotosu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etkinlik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rolu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullanicilar", x => new { x.Id, x.Eposta });
                    table.UniqueConstraint("AK_kullanicilar_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mesajlasmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderenId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<int>(type: "int", nullable: false),
                    Mesaj = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesajlasmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "antrenorler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UzmanlikAlanlari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deneyimleri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_antrenorler", x => new { x.Id, x.Eposta });
                    table.UniqueConstraint("AK_antrenorler_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_antrenorler_kullanicilar_Id",
                        column: x => x.Id,
                        principalTable: "kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gelismeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YapanId = table.Column<int>(type: "int", nullable: false),
                    Kilo = table.Column<float>(type: "real", nullable: false),
                    Boy = table.Column<int>(type: "int", nullable: false),
                    VucutYagOrani = table.Column<float>(type: "real", nullable: false),
                    KasKütlesi = table.Column<float>(type: "real", nullable: false),
                    VucutKitleIndeksi = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gelismeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gelismeler_kullanicilar_YapanId",
                        column: x => x.YapanId,
                        principalTable: "kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "antrenmanProgramlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EgsersizAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hedef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetTekrarSayilari = table.Column<int>(type: "int", nullable: false),
                    VideoRehberleri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrograminSuresi = table.Column<int>(type: "int", nullable: false),
                    YapanId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_antrenmanProgramlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_antrenmanProgramlari_antrenorler_YapanId",
                        column: x => x.YapanId,
                        principalTable: "antrenorler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_antrenmanProgramlari_kullanicilar_AliciId",
                        column: x => x.AliciId,
                        principalTable: "kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "beslenmeProgramlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hedef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GunlukOgunler = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Kalori = table.Column<int>(type: "int", nullable: false),
                    YapanId = table.Column<int>(type: "int", nullable: false),
                    AliciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beslenmeProgramlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_beslenmeProgramlari_antrenorler_YapanId",
                        column: x => x.YapanId,
                        principalTable: "antrenorler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_beslenmeProgramlari_kullanicilar_AliciId",
                        column: x => x.AliciId,
                        principalTable: "kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_antrenmanProgramlari_AliciId",
                table: "antrenmanProgramlari",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_antrenmanProgramlari_YapanId",
                table: "antrenmanProgramlari",
                column: "YapanId");

            migrationBuilder.CreateIndex(
                name: "IX_beslenmeProgramlari_AliciId",
                table: "beslenmeProgramlari",
                column: "AliciId");

            migrationBuilder.CreateIndex(
                name: "IX_beslenmeProgramlari_YapanId",
                table: "beslenmeProgramlari",
                column: "YapanId");

            migrationBuilder.CreateIndex(
                name: "IX_gelismeler_YapanId",
                table: "gelismeler",
                column: "YapanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "antrenmanProgramlari");

            migrationBuilder.DropTable(
                name: "beslenmeProgramlari");

            migrationBuilder.DropTable(
                name: "gelismeler");

            migrationBuilder.DropTable(
                name: "kullaniciAntrenor");

            migrationBuilder.DropTable(
                name: "mesajlasmalar");

            migrationBuilder.DropTable(
                name: "antrenorler");

            migrationBuilder.DropTable(
                name: "kullanicilar");
        }
    }
}
