using API.Controllers.DTO;
using Application.DTO;
using Domain;
using Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                List<Payment_Schedule> payment_ = await this.context.Payment_Schedule.ToListAsync();
                payment_ = payment_.OrderBy(x => x.Loan_Id).ToList();
                payment_ = payment_.FindAll(x=>x.Loan_Id==request.loanBeneficiaryDTO.loanId);
                
                if(!payment_[0].Escrow) {
                    return Unit.Value;
                }

                LoanDetails loan = await this.context.LoanDetails.FindAsync(request.loanBeneficiaryDTO.loanId);
                decimal annualPaymnet = 0;

                BeneficiaryDTO beneficiary_1 = request.loanBeneficiaryDTO.beneficiary_1;
                annualPaymnet = await findAnnualPaymentAsync(beneficiary_1);

                BeneficiaryDTO beneficiary_2 = request.loanBeneficiaryDTO.beneficiary_2;
                annualPaymnet += await findAnnualPaymentAsync(beneficiary_2);

                BeneficiaryDTO beneficiary_3 = request.loanBeneficiaryDTO.beneficiary_3;
                annualPaymnet += await findAnnualPaymentAsync(beneficiary_3);

                BeneficiaryDTO beneficiary_4 = request.loanBeneficiaryDTO.beneficiary_4;
                annualPaymnet += await findAnnualPaymentAsync(beneficiary_4);

                BeneficiaryDTO beneficiary_5 = request.loanBeneficiaryDTO.beneficiary_5;
                annualPaymnet += await findAnnualPaymentAsync(beneficiary_5);

                decimal monthlyPayment = annualPaymnet/12;

                List<Escrow_Disbursement_Schedule> dueList = new List<Escrow_Disbursement_Schedule>();

                decimal previousBalance = 0;
                for (int i = 0; i < 12; i++)
                {
                    if(dueList.Count()!=0) previousBalance = dueList.Last().Escrow_Balance;
                    Escrow_Disbursement_Schedule Dues = EscPayment(loan, monthlyPayment, previousBalance);
                    Dues.date = Dues.date.AddMonths(i+1);
                    dueList.Add(Dues);
                }
                // await this.context.AddRangeAsync(dueList);
                // await this.context.SaveChangesAsync();

                // List<Escrow_Disbursement_Schedule> dueList = new List<Escrow_Disbursement_Schedule>();
                int n=12;
                Benificiary ben1 =  this.context.Benificiary.Find(beneficiary_1.BeneficiaryId);
                if(ben1.frequency=="ANNUALLY") 
                {
                    n=1;
                }
                else if(ben1.frequency=="QUARTERLY"){ n=4;}
                int num = 12/n;
                for (int i = 0; i < n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_1, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_1.BeneficiaryId;
                    disbursement.disbursement_frequency = ben1.frequency;
                    dueList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben2 =  this.context.Benificiary.Find(beneficiary_2.BeneficiaryId);
                if(ben2.frequency == "ANNUALLY") n=1;
                else if(ben2.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_2, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_2.BeneficiaryId;
                    disbursement.disbursement_frequency = ben2.frequency;
                    dueList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben3 =  this.context.Benificiary.Find(beneficiary_3.BeneficiaryId);
                if(ben3.frequency == "ANNUALLY") n=1;
                else if(ben3.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_3, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_3.BeneficiaryId;
                    disbursement.disbursement_frequency = ben3.frequency;
                    dueList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben4 =  this.context.Benificiary.Find(beneficiary_4.BeneficiaryId);
                if(ben4.frequency == "ANNUALLY") n=1;
                else if(ben4.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_4, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_4.BeneficiaryId;
                    disbursement.disbursement_frequency = ben4.frequency;
                    dueList.Add(disbursement);
                    num += 12/n;
                }

                n=12;
                Benificiary ben5 =  this.context.Benificiary.Find(beneficiary_5.BeneficiaryId);
                if(ben5.frequency == "ANNUALLY") n=1;
                else if(ben5.frequency == "QUARTERLY") n=4;
                num = 12/n;
                for (int i = 1; i <= n; i++)
                {
                    Escrow_Disbursement_Schedule disbursement = await EscDisbursement(beneficiary_5, loan.LoanId);
                    disbursement.Loan_Id = loan.LoanId;
                    disbursement.date = disbursement.date.AddMonths(num);
                    disbursement.beneficiary_id = beneficiary_5.BeneficiaryId;
                    disbursement.disbursement_frequency = ben5.frequency;
                    dueList.Add(disbursement);
                    num += 12/n;
                }
                // await this.context.AddRangeAsync(dueList);
                // await this.context.SaveChangesAsync();

                // List<Escrow_Disbursement_Schedule> escrows = await this.context.Escrow_Disbursement_Schedule.ToListAsync();
                dueList = dueList.OrderBy(x=>x.Loan_Id).ThenBy(x=>x.date).ToList();
                // dueList.OrderBy(x=>x.Loan_Id).ThenBy(x=>x.date);
                int lastLoanId = 0;
                decimal lastBal = 0;
                foreach (var item in dueList)
                {
                    if(item.Loan_Id != lastLoanId) {
                        lastBal = 0;
                    }
                    decimal currentBal = lastBal + item.escrow_payment_amount - item.escrow_disbursement;
                    item.Escrow_Balance = currentBal;
                    // this.context.Update(item);
                    lastBal = currentBal;
                    lastLoanId = item.Loan_Id;
                }
                await this.context.AddRangeAsync(dueList);
                await this.context.SaveChangesAsync();

                List<Payment_Schedule> payment = await this.context.Payment_Schedule.ToListAsync();
                List<Payment_Schedule> list = new List<Payment_Schedule>();
                foreach (var item in payment)
                {
                    if(item.Loan_Id == request.loanBeneficiaryDTO.loanId) {
                        item.Escrow_Amount = monthlyPayment;
                        item.Monthly_Payment_Amount = item.Principal_Amount + item.Interest_Amount + item.Escrow_Amount;
                        list.Add(item);
                    }
                }
                await this.context.AddRangeAsync();
                await this.context.SaveChangesAsync();

                return Unit.Value;
            }
            public async Task<decimal> findAnnualPaymentAsync(BeneficiaryDTO beneficiaryDto) {
                Benificiary beneficiary = await this.context.Benificiary.FindAsync(beneficiaryDto.BeneficiaryId);
                string frequency = beneficiary.frequency;

                decimal n = 1;
                if (frequency == "MONTHLY")
                {
                    n = 12;
                }
                else if(frequency == "QUARTERLY") {
                    n = 4;
                }

                decimal disbursementAmount = beneficiaryDto.disbursementAmount;

                decimal escPayment = disbursementAmount*n;

                return escPayment;
            }

            public Escrow_Disbursement_Schedule EscPayment(LoanDetails loan, decimal monthlyPayment, decimal previousBalance) {
                Escrow_Disbursement_Schedule escPayment = new Escrow_Disbursement_Schedule();

                escPayment.date = loan.PmtDueDate;
                escPayment.escrow_payment_amount = monthlyPayment;

                // Escrow_Disbursement_Schedule previousBalance = this.context.Escrow_Disbursement_Schedule.OrderBy(x=>x.id).LastOrDefault(x => x.Loan_Id == loan.LoanId);
                

                escPayment.Escrow_Balance = previousBalance + monthlyPayment;
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

                List<Escrow_Disbursement_Schedule> previousBalance = await this.context.Escrow_Disbursement_Schedule.ToListAsync();
                previousBalance = previousBalance.FindAll(x=>x.Loan_Id == loanId);
                previousBalance = previousBalance.OrderBy(x=>x.date).ToList();
                decimal lastBalance = 0;
                if(previousBalance.Count !=0 ) lastBalance = previousBalance.Last().Escrow_Balance;

                escDisburse.Escrow_Balance = lastBalance - beneficiaryDto.disbursementAmount;

                return escDisburse;
            }
        }
    }
}