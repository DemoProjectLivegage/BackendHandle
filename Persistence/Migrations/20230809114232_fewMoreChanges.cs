using System;
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
                name: "Borrowers",
                columns: table => new
                {
                    Borrower_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Full_Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact_Number = table.Column<int>(type: "int", nullable: false),
                    Mailing_Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Zip_code = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Occupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowers", x => x.Borrower_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Loan_Details",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PI_Pmt_Amt = table.Column<int>(type: "int", nullable: false),
                    UPB_Amt = table.Column<int>(type: "int", nullable: false),
                    Remaining_EMI = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Pmt_Due_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Property_Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_Details", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Loan_Information",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Prior_Servicer_Loan_Id = table.Column<int>(type: "int", nullable: false),
                    Note_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Loan_Boarding_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Note_Rate_Percent = table.Column<int>(type: "int", nullable: false),
                    Escrow = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tax_Insurance_Pmt_Amt = table.Column<int>(type: "int", nullable: false),
                    Total_Loan_Amount = table.Column<int>(type: "int", nullable: false),
                    Loan_Term = table.Column<int>(type: "int", nullable: false),
                    Loan_Type = table.Column<int>(type: "int", nullable: false),
                    Payment_Freq = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Primary_Contact = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_Information", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_Information_Loan_Details_Id",
                        column: x => x.Id,
                        principalTable: "Loan_Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Borrower_DetailsLoan_Information",
                columns: table => new
                {
                    Borrower_DetailsBorrower_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Loan_InformationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrower_DetailsLoan_Information", x => new { x.Borrower_DetailsBorrower_Id, x.Loan_InformationId });
                    table.ForeignKey(
                        name: "FK_Borrower_DetailsLoan_Information_Borrowers_Borrower_DetailsB~",
                        column: x => x.Borrower_DetailsBorrower_Id,
                        principalTable: "Borrowers",
                        principalColumn: "Borrower_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrower_DetailsLoan_Information_Loan_Information_Loan_Infor~",
                        column: x => x.Loan_InformationId,
                        principalTable: "Loan_Information",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Borrower_DetailsLoan_Information_Loan_InformationId",
                table: "Borrower_DetailsLoan_Information",
                column: "Loan_InformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrower_DetailsLoan_Information");

            migrationBuilder.DropTable(
                name: "Borrowers");

            migrationBuilder.DropTable(
                name: "Loan_Information");

            migrationBuilder.DropTable(
                name: "Loan_Details");
        }
    }
}
