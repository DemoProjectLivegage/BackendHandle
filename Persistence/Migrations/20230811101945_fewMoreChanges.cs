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
                name: "BorrowersDetails",
                columns: table => new
                {
                    BorrowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumber = table.Column<int>(type: "int", nullable: false),
                    MailingAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Zipcode = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Occupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowersDetails", x => x.BorrowerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoanDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PIPmtAmt = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UPBAmt = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RemainingPayments = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PmtDueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PropertyAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetails", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoanInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Prior_Servicer_Loan_Id = table.Column<int>(type: "int", nullable: false),
                    NoteDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LoanBoardingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NoteRatePercent = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Escrow = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TaxInsurancePmtAmt = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalLoanAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LoanTerm = table.Column<int>(type: "int", nullable: false),
                    LoanType = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PaymentFreq = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrimaryContact = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanInformation_LoanDetails_Id",
                        column: x => x.Id,
                        principalTable: "LoanDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BorrowerDetailsLoanInformation",
                columns: table => new
                {
                    BorrowerDetailsBorrowerId = table.Column<int>(type: "int", nullable: false),
                    LoanInformationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowerDetailsLoanInformation", x => new { x.BorrowerDetailsBorrowerId, x.LoanInformationId });
                    table.ForeignKey(
                        name: "FK_BorrowerDetailsLoanInformation_BorrowersDetails_BorrowerDeta~",
                        column: x => x.BorrowerDetailsBorrowerId,
                        principalTable: "BorrowersDetails",
                        principalColumn: "BorrowerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowerDetailsLoanInformation_LoanInformation_LoanInformati~",
                        column: x => x.LoanInformationId,
                        principalTable: "LoanInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowerDetailsLoanInformation_LoanInformationId",
                table: "BorrowerDetailsLoanInformation",
                column: "LoanInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowerDetailsLoanInformation");

            migrationBuilder.DropTable(
                name: "BorrowersDetails");

            migrationBuilder.DropTable(
                name: "LoanInformation");

            migrationBuilder.DropTable(
                name: "LoanDetails");
        }
    }
}
