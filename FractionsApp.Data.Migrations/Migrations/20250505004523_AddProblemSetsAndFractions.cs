using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;


#nullable disable

namespace FractionsApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProblemSetsAndFractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Numerator = table.Column<int>(type: "integer", nullable: false),
                    Denominator = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fractions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProblemSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FractionProblems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProblemSetId = table.Column<int>(type: "integer", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    Options = table.Column<string>(type: "text", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: false),
                    Hint = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Operand1Id = table.Column<int>(type: "integer", nullable: false),
                    Operand2Id = table.Column<int>(type: "integer", nullable: false),
                    Operation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FractionProblems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FractionProblems_Fractions_Operand1Id",
                        column: x => x.Operand1Id,
                        principalTable: "Fractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FractionProblems_Fractions_Operand2Id",
                        column: x => x.Operand2Id,
                        principalTable: "Fractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FractionProblems_ProblemSets_ProblemSetId",
                        column: x => x.ProblemSetId,
                        principalTable: "ProblemSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FractionProblems_Operand1Id",
                table: "FractionProblems",
                column: "Operand1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FractionProblems_Operand2Id",
                table: "FractionProblems",
                column: "Operand2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FractionProblems_ProblemSetId",
                table: "FractionProblems",
                column: "ProblemSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FractionProblems");

            migrationBuilder.DropTable(
                name: "Fractions");

            migrationBuilder.DropTable(
                name: "ProblemSets");
        }
    }
}
