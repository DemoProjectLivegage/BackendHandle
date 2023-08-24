using API.Controllers.DTO;
using Application.DTO;
using Domain;
using Persistence;
using MediatR;

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

                List<Escrow_Disbursement_Schedule> dueList = new List<Escrow_Disbursement_Schedule>();
                for (int i = 0; i < 12; i++)
                {
                    Escrow_Disbursement_Schedule Dues = EscPayment(loan, monthlyPayment);
                    Dues.date = Dues.date.AddMonths(i+1);
                    dueList.Add(Dues);
                }
                await this.context.AddRangeAsync(dueList);
                await this.context.SaveChangesAsync();

                List<Escrow_Disbursement_Schedule> disbursementList = new List<Escrow_Disbursement_Schedule>();
                int n=12;
                Benificiary ben1 = await this.context.Benificiary.FindAsync(beneficiary_1.BeneficiaryId);
                if(ben1.frequency == "ANNUALLY") n=1;
                else if(ben1.frequency == "QUARTERLY") n=4;
                int num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_1, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_1.BeneficiaryId;
                    disbursement.disbursement_frequency = ben1.frequency;
                    disbursementList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben2 = await this.context.Benificiary.FindAsync(beneficiary_1.BeneficiaryId);
                if(ben2.frequency == "ANNUALLY") n=1;
                else if(ben2.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_1, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_2.BeneficiaryId;
                    disbursement.disbursement_frequency = ben2.frequency;
                    disbursementList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben3 = await this.context.Benificiary.FindAsync(beneficiary_1.BeneficiaryId);
                if(ben3.frequency == "ANNUALLY") n=1;
                else if(ben3.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_1, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_3.BeneficiaryId;
                    disbursement.disbursement_frequency = ben3.frequency;
                    disbursementList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben4 = await this.context.Benificiary.FindAsync(beneficiary_1.BeneficiaryId);
                if(ben4.frequency == "ANNUALLY") n=1;
                else if(ben4.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_1, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_4.BeneficiaryId;
                    disbursement.disbursement_frequency = ben4.frequency;
                    disbursementList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben5 = await this.context.Benificiary.FindAsync(beneficiary_1.BeneficiaryId);
                if(ben5.frequency == "ANNUALLY") n=1;
                else if(ben5.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_1, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_5.BeneficiaryId;
                    disbursement.disbursement_frequency = ben5.frequency;
                    disbursementList.Add(disbursement);
                    num += 12/n;
                }
                await this.context.AddRangeAsync(disbursementList);
                await this.context.SaveChangesAsync();

                Escrow_Disbursement_Schedule schedule = await this.context.Escrow_Disbursement_Schedule.FindAsync();

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
                escPayment.escrow_payment_amount = monthlyPayment;

                Escrow_Disbursement_Schedule previousBalance = this.context.Escrow_Disbursement_Schedule.OrderBy(x=>x.id).LastOrDefault(x => x.Loan_Id == loan.LoanId);
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

                escDisburse.escrow_disbursement = beneficiaryDto.disbursementAmount;

                Escrow_Disbursement_Schedule previousBalance = this.context.Escrow_Disbursement_Schedule.OrderBy(x=>x.id).LastOrDefault(x => x.Loan_Id == loanId);
                decimal lastBalance = 0;
                if(previousBalance != null) lastBalance = previousBalance.Escrow_Balance;

                escDisburse.Escrow_Balance = lastBalance - beneficiaryDto.disbursementAmount;

                return escDisburse;
            }
        }
    }
}