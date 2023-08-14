using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fewMoreChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoanInformation",
                columns: table => new
                {
                    LoanInformationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PriorServicerLoanId = table.Column<int>(type: "int", nullable: false),
                    NoteDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LoanBoardingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NoteRatePercent = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Escrow = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TaxInsurancePmtAmt = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalLoanAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LoanTerm = table.Column<int>(type: "int", nullable: false),
                    LoanType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentFreq = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrimaryContact = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInformation", x => x.LoanInformationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BorrowersDetails",
                columns: table => new
                {
                    BorrowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MailingAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Zipcode = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Occupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoanInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowersDetails", x => x.BorrowerId);
                    table.ForeignKey(
                        name: "FK_BorrowersDetails_LoanInformation_LoanInformationId",
                        column: x => x.LoanInformationId,
                        principalTable: "LoanInformation",
                        principalColumn: "LoanInformationId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoanDetails",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PIPmtAmt = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UPBAmt = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RemainingPayments = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PmtDueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PropertyAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoanInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetails", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_LoanDetails_LoanInformation_LoanInformationId",
                        column: x => x.LoanInformationId,
                        principalTable: "LoanInformation",
                        principalColumn: "LoanInformationId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowersDetails_LoanInformationId",
                table: "BorrowersDetails",
                column: "LoanInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_LoanInformationId",
                table: "LoanDetails",
                column: "LoanInformationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowersDetails");

            migrationBuilder.DropTable(
                name: "LoanDetails");

            migrationBuilder.DropTable(
                name: "LoanInformation");
        }
    }
}
