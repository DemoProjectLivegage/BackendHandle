using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Persistence;

namespace Application.Payment_Hierarchy_
{
    public class CreatePaymentGL
    {
        public static void map(DatabaseContext context, Payment_Hierarchy data, string type)
        {
            // context.UserTransactions.Add();

            List<AllGeneralLedger> gl_list = new List<AllGeneralLedger>();
            if (type == "all")
            {
                Transactions t_suspence = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("suspence")).FirstOrDefault();
                Transactions t_principal = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("principal")).FirstOrDefault();
                Transactions t_interest = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("interest")).FirstOrDefault();
                Transactions t_escrow = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("escrow")).FirstOrDefault();
                Transactions t_late = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("late")).FirstOrDefault();
                Transactions t_other = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("other")).FirstOrDefault();
                Transactions t_upb = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("upb")).FirstOrDefault();
                Transactions t_monthly = context.Transaction.Where(x => x.transaction_name.ToLower().Contains("monthly")).FirstOrDefault();
                if (data.Monthly_Payment_Amount > 0 && t_monthly != null)
                {
                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.Monthly_Payment_Amount;
                    gl.to_account_balance = data.Monthly_Payment_Amount;
                    gl.Transaction = t_monthly.Id;
                    gl_list.Add(gl);

                }
                if (data.suspence > 0 && t_suspence != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.suspence;
                    gl.to_account_balance = data.suspence;
                    gl.Transaction = t_suspence.Id;
                    gl_list.Add(gl);

                }
                if (data.principal > 0 && t_principal != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.principal;
                    gl.to_account_balance = data.principal;
                    gl.Transaction = t_principal.Id;
                    gl_list.Add(gl);

                }
                if (data.escrow > 0 && t_escrow != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.escrow;
                    gl.to_account_balance = data.escrow;
                    gl.Transaction = t_escrow.Id;
                    gl_list.Add(gl);

                }
                if (data.interest > 0 && t_interest != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.interest;
                    gl.to_account_balance = data.interest;
                    gl.Transaction = t_interest.Id;
                    gl_list.Add(gl);

                }
                if (data.other_fee > 0 && t_other != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.other_fee;
                    gl.to_account_balance = data.other_fee;
                    gl.Transaction = t_other.Id;
                    gl_list.Add(gl);

                }
                if (data.late_charge > 0 && t_late != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.late_charge;
                    gl.to_account_balance = data.late_charge;
                    gl.Transaction = t_late.Id;
                    gl_list.Add(gl);

                }
                if (data.UPB_Amount > 0 && t_upb != null)
                {

                    AllGeneralLedger gl = new AllGeneralLedger();

                    gl.LoanId = data.Loan_id;
                    gl.from_account_balance = data.UPB_Amount;
                    gl.to_account_balance = data.UPB_Amount;
                    gl.Transaction = t_upb.Id;
                    gl_list.Add(gl);

                }
            }
            else
            {
                AllGeneralLedger gl = new AllGeneralLedger();

                gl.LoanId = data.Loan_id;
                Transactions transaction = context.Transaction.Where(x => x.transaction_name.ToLower().Contains(type)).FirstOrDefault();
                gl.from_account_balance = data.suspence;
                gl.to_account_balance = data.suspence;
                gl.Transaction = transaction.Id;
                gl_list.Add(gl);

            }
            context.UserTransactions.AddRange(gl_list);
            context.SaveChanges();
        }
    }
}