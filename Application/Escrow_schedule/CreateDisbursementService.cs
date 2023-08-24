using API.Controllers.DTO;
using Application.DTO;
using Domain;
using MediatR;
using Persistence;

namespace Application.Escrow_schedule
{
    public class CreateDisbursementService
    {
        public class Command : IRequest {
            public LoanBeneficiaryDTO loanBeneficiaryDTO {get; set;}
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DatabaseContext context;
            public Handler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                LoanDetails loan = await this.context.LoanDetails.FindAsync(request.loanBeneficiaryDTO.loanId);

                BeneficiaryDTO beneficiary_1 = request.loanBeneficiaryDTO.beneficiary_1;
                decimal annualPaymnet = await findAnnualPayment(beneficiary_1);

                BeneficiaryDTO beneficiary_2 = request.loanBeneficiaryDTO.beneficiary_2;
                annualPaymnet = annualPaymnet + await findAnnualPayment(beneficiary_1);

                BeneficiaryDTO beneficiary_3 = request.loanBeneficiaryDTO.beneficiary_3;
                annualPaymnet = annualPaymnet + await findAnnualPayment(beneficiary_1);

                BeneficiaryDTO beneficiary_4 = request.loanBeneficiaryDTO.beneficiary_4;
                annualPaymnet = annualPaymnet + await findAnnualPayment(beneficiary_1);

                BeneficiaryDTO beneficiary_5 = request.loanBeneficiaryDTO.beneficiary_5;
                annualPaymnet = annualPaymnet + await findAnnualPayment(beneficiary_1);

                decimal monthlyPayment = annualPaymnet/12;

                Escrow_Disbursement_Schedule Dues = EscPayment(loan, monthlyPayment);
                await this.context.AddAsync(Dues);
                await this.context.SaveChangesAsync();

                Escrow_Disbursement_Schedule disbursement_1 = await EscDisbursement(beneficiary_1, loan.LoanId);
                disbursement_1.Loan_Id = loan.LoanId;
                await this.context.AddAsync(disbursement_1);
                await this.context.SaveChangesAsync();

                Escrow_Disbursement_Schedule disbursement_2 = await EscDisbursement(beneficiary_2, loan.LoanId);
                disbursement_2.Loan_Id = loan.LoanId;
                await this.context.AddAsync(disbursement_2);
                await this.context.SaveChangesAsync();

                Escrow_Disbursement_Schedule disbursement_3 = await EscDisbursement(beneficiary_3, loan.LoanId);
                disbursement_3.Loan_Id = loan.LoanId;
                await this.context.AddAsync(disbursement_3);
                await this.context.SaveChangesAsync();

                Escrow_Disbursement_Schedule disbursement_4 = await EscDisbursement(beneficiary_4, loan.LoanId);
                disbursement_4.Loan_Id = loan.LoanId;
                await this.context.AddAsync(disbursement_4);
                await this.context.SaveChangesAsync();

                Escrow_Disbursement_Schedule disbursement_5 = await EscDisbursement(beneficiary_5, loan.LoanId);
                disbursement_5.Loan_Id = loan.LoanId;
                await this.context.AddAsync(disbursement_5);

                await this.context.SaveChangesAsync();

                return Unit.Value;
            }
            public async Task<decimal> findAnnualPayment(BeneficiaryDTO beneficiaryDto) {
                Benificiary beneficiary = await this.context.Benificiary.FindAsync(beneficiaryDto.BeneficiaryId);
                string frequency = beneficiary.frequency;

                decimal n = 1;
                if (frequency == "MONTHLY")
                {
                    n = 12;
                }
                else if(frequency == "QUARTERLLY") {
                    n = 4;
                }

                decimal disbursementAmount = beneficiaryDto.disbursementAmount;

                decimal escPayment = disbursementAmount*n;

                return escPayment;
            }

            public Escrow_Disbursement_Schedule EscPayment(LoanDetails loan, decimal monthlyPayment) {
                Escrow_Disbursement_Schedule escPayment = new Escrow_Disbursement_Schedule();

                escPayment.date = loan.PmtDueDate;
                escPayment.Incoming_Escrow = monthlyPayment;

                Escrow_Disbursement_Schedule previousBalance = this.context.Escrow_Disbursement.OrderBy(x=>x.Escrow_Id).LastOrDefault(x => x.Loan_Id == loan.LoanId);
                decimal lastBalance = 0;
                if(previousBalance != null) lastBalance = previousBalance.Escrow_Balance;

                escPayment.Escrow_Balance = lastBalance + monthlyPayment;
                escPayment.Loan_Id = loan.LoanId;

                return escPayment;
            }
            public async Task<Escrow_Disbursement_Schedule> EscDisbursement(BeneficiaryDTO beneficiaryDto, int loanId) {
                Escrow_Disbursement_Schedule escDisburse = new Escrow_Disbursement_Schedule();

                escDisburse.date = beneficiaryDto.disbursementDate;
                Benificiary beneficiary = await this.context.Benificiary.FindAsync(beneficiaryDto.BeneficiaryId);

                escDisburse.Escrow_Name = beneficiary.name;
                string frequency = beneficiary.frequency;

                escDisburse.Escrow_Disbursement = beneficiaryDto.disbursementAmount;

                Escrow_Disbursement_Schedule previousBalance = this.context.Escrow_Disbursement.OrderBy(x=>x.Escrow_Id).LastOrDefault(x => x.Loan_Id == loanId);
                decimal lastBalance = 0;
                if(previousBalance != null) lastBalance = previousBalance.Escrow_Balance;

                escDisburse.Escrow_Balance = lastBalance - beneficiaryDto.disbursementAmount;

                return escDisburse;
            }
        }
    }
}