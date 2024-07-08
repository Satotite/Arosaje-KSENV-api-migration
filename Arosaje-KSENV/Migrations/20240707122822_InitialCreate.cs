using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Arosaje_KSENV.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dateTips",
                columns: table => new
                {
                    Id_Tips = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contenu = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__dateTips__0641639EAF8D1B48", x => x.Id_Tips);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id_Message = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contenu = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    dateMessage = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__A33138E696D85CD1", x => x.Id_Message);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id_Photo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    image = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    extension = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Photo__96CEBB73EF77EAA1", x => x.Id_Photo);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Mdp = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Prenom = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Utilisat__63A5C1FF9ADAA4AE", x => x.Id_Utilisateur);
                });

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    Id_Ville = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Desc = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ville__EF0C38FCB16C0CA0", x => x.Id_Ville);
                });

            migrationBuilder.CreateTable(
                name: "Botaniste",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Botanist__63A5C1FFBCC7A728", x => x.Id_Utilisateur);
                    table.ForeignKey(
                        name: "FK__Botaniste__Id_Ut__3C69FB99",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "Id_Utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Membre",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Membre__63A5C1FFA80227E1", x => x.Id_Utilisateur);
                    table.ForeignKey(
                        name: "FK__Membre__Id_Utili__398D8EEE",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "Id_Utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Proprio",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proprio__63A5C1FFDE3A6090", x => x.Id_Utilisateur);
                    table.ForeignKey(
                        name: "FK__Proprio__Id_Util__46E78A0C",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "Id_Utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Habite",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false),
                    Id_Ville = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habite__BD550270470E1D44", x => new { x.Id_Utilisateur, x.Id_Ville });
                    table.ForeignKey(
                        name: "FK__Habite__Id_Utili__60A75C0F",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "Id_Utilisateur");
                    table.ForeignKey(
                        name: "FK__Habite__Id_Ville__619B8048",
                        column: x => x.Id_Ville,
                        principalTable: "Ville",
                        principalColumn: "Id_Ville");
                });

            migrationBuilder.CreateTable(
                name: "Conseiller",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false),
                    Id_Tips = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Conseill__93C1D7C6ECFA37EC", x => new { x.Id_Utilisateur, x.Id_Tips });
                    table.ForeignKey(
                        name: "FK__Conseille__Id_Ti__59FA5E80",
                        column: x => x.Id_Tips,
                        principalTable: "dateTips",
                        principalColumn: "Id_Tips");
                    table.ForeignKey(
                        name: "FK__Conseille__Id_Ut__59063A47",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Botaniste",
                        principalColumn: "Id_Utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Plante",
                columns: table => new
                {
                    Id_Plante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Espece = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Categorie = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Etat = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Nom = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Lon = table.Column<string>(type: "text", nullable: true),
                    Lat = table.Column<string>(type: "text", nullable: true),
                    Id_Ville = table.Column<int>(type: "integer", nullable: false),
                    Id_Photo = table.Column<int>(type: "integer", nullable: true),
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false),
                    Id_Utilisateur_1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plante__8E9A9EE58E900046", x => x.Id_Plante);
                    table.ForeignKey(
                        name: "FK__Plante__Id_Photo__4AB81AF0",
                        column: x => x.Id_Photo,
                        principalTable: "Photo",
                        principalColumn: "Id_Photo");
                    table.ForeignKey(
                        name: "FK__Plante__Id_Utili__4BAC3F29",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Membre",
                        principalColumn: "Id_Utilisateur");
                    table.ForeignKey(
                        name: "FK__Plante__Id_Utili__4CA06362",
                        column: x => x.Id_Utilisateur_1,
                        principalTable: "Membre",
                        principalColumn: "Id_Utilisateur");
                    table.ForeignKey(
                        name: "FK__Plante__Id_Ville__49C3F6B7",
                        column: x => x.Id_Ville,
                        principalTable: "Ville",
                        principalColumn: "Id_Ville");
                });

            migrationBuilder.CreateTable(
                name: "Prendre",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false),
                    Id_Photo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prendre__4AC92A48BE6E6FB2", x => new { x.Id_Utilisateur, x.Id_Photo });
                    table.ForeignKey(
                        name: "FK__Prendre__Id_Phot__5070F446",
                        column: x => x.Id_Photo,
                        principalTable: "Photo",
                        principalColumn: "Id_Photo");
                    table.ForeignKey(
                        name: "FK__Prendre__Id_Util__4F7CD00D",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Membre",
                        principalColumn: "Id_Utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Envoyer_recevoir",
                columns: table => new
                {
                    Id_Utilisateur = table.Column<int>(type: "integer", nullable: false),
                    Id_Utilisateur_1 = table.Column<int>(type: "integer", nullable: false),
                    Id_Utilisateur_2 = table.Column<int>(type: "integer", nullable: false),
                    Id_Message = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Envoyer___97E4F897273813C5", x => new { x.Id_Utilisateur, x.Id_Utilisateur_1, x.Id_Utilisateur_2, x.Id_Message });
                    table.ForeignKey(
                        name: "FK__Envoyer_r__Id_Me__5629CD9C",
                        column: x => x.Id_Message,
                        principalTable: "Message",
                        principalColumn: "Id_Message");
                    table.ForeignKey(
                        name: "FK__Envoyer_r__Id_Ut__534D60F1",
                        column: x => x.Id_Utilisateur,
                        principalTable: "Membre",
                        principalColumn: "Id_Utilisateur");
                    table.ForeignKey(
                        name: "FK__Envoyer_r__Id_Ut__5441852A",
                        column: x => x.Id_Utilisateur_1,
                        principalTable: "Botaniste",
                        principalColumn: "Id_Utilisateur");
                    table.ForeignKey(
                        name: "FK__Envoyer_r__Id_Ut__5535A963",
                        column: x => x.Id_Utilisateur_2,
                        principalTable: "Proprio",
                        principalColumn: "Id_Utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "HasTips",
                columns: table => new
                {
                    Id_Plante = table.Column<int>(type: "integer", nullable: false),
                    Id_Tips = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HasTips__7EFE88DC09331A83", x => new { x.Id_Plante, x.Id_Tips });
                    table.ForeignKey(
                        name: "FK__HasTips__Id_Plan__5CD6CB2B",
                        column: x => x.Id_Plante,
                        principalTable: "Plante",
                        principalColumn: "Id_Plante");
                    table.ForeignKey(
                        name: "FK__HasTips__Id_Tips__5DCAEF64",
                        column: x => x.Id_Tips,
                        principalTable: "dateTips",
                        principalColumn: "Id_Tips");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conseiller_Id_Tips",
                table: "Conseiller",
                column: "Id_Tips");

            migrationBuilder.CreateIndex(
                name: "IX_Envoyer_recevoir_Id_Message",
                table: "Envoyer_recevoir",
                column: "Id_Message");

            migrationBuilder.CreateIndex(
                name: "IX_Envoyer_recevoir_Id_Utilisateur_1",
                table: "Envoyer_recevoir",
                column: "Id_Utilisateur_1");

            migrationBuilder.CreateIndex(
                name: "IX_Envoyer_recevoir_Id_Utilisateur_2",
                table: "Envoyer_recevoir",
                column: "Id_Utilisateur_2");

            migrationBuilder.CreateIndex(
                name: "IX_Habite_Id_Ville",
                table: "Habite",
                column: "Id_Ville");

            migrationBuilder.CreateIndex(
                name: "IX_HasTips_Id_Tips",
                table: "HasTips",
                column: "Id_Tips");

            migrationBuilder.CreateIndex(
                name: "IX_Plante_Id_Photo",
                table: "Plante",
                column: "Id_Photo");

            migrationBuilder.CreateIndex(
                name: "IX_Plante_Id_Utilisateur",
                table: "Plante",
                column: "Id_Utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Plante_Id_Utilisateur_1",
                table: "Plante",
                column: "Id_Utilisateur_1");

            migrationBuilder.CreateIndex(
                name: "IX_Plante_Id_Ville",
                table: "Plante",
                column: "Id_Ville");

            migrationBuilder.CreateIndex(
                name: "IX_Prendre_Id_Photo",
                table: "Prendre",
                column: "Id_Photo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conseiller");

            migrationBuilder.DropTable(
                name: "Envoyer_recevoir");

            migrationBuilder.DropTable(
                name: "Habite");

            migrationBuilder.DropTable(
                name: "HasTips");

            migrationBuilder.DropTable(
                name: "Prendre");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Botaniste");

            migrationBuilder.DropTable(
                name: "Proprio");

            migrationBuilder.DropTable(
                name: "Plante");

            migrationBuilder.DropTable(
                name: "dateTips");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Membre");

            migrationBuilder.DropTable(
                name: "Ville");

            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
