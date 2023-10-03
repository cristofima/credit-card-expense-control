using CreditCardExpenseControl.API.DbContexts;
using CreditCardExpenseControl.API.Entities;
using CreditCardExpenseControl.API.Models;
using CreditCardExpenseControl.API.Utils;
using Microsoft.EntityFrameworkCore;

namespace CreditCardExpenseControl.API.Services
{
    public class ReportService : IReportService
    {
        private readonly CreditCardExpenseControlContext _context;

        public ReportService(CreditCardExpenseControlContext context)
        {
            _context = context;
        }

        public List<ReportModel> GetTransactionsReportByYear(int year)
        {
            var creditCards = _context.CreditCards.AsNoTracking().ToList();
            var transactions = _context.Transactions.Select(t => new ReportTransactionModel
            {
                Quotas = t.Quotas,
                Amount = t.Amount,
                Date = t.Date,
                CreditCard = new CreditCardModel
                {
                    Brand = t.CreditCard.Brand,
                    Id = t.CreditCard.Id,
                    Last4Digits = t.CreditCard.Last4Digits,
                    Name = t.CreditCard.Name
                },
                AproxMonthlyQuota = t.Amount / t.Quotas,
                Description = t.Description,
                Id = t.Id
            }).AsNoTracking().ToList();

            int[] months = Enumerable.Range(1, 12).ToArray();

            return creditCards.Select(creditCard =>
            {
                var rm = new ReportModel
                {
                    CreditCard = new CreditCardModel
                    {
                        Brand = creditCard.Brand,
                        Id = creditCard.Id,
                        Last4Digits = creditCard.Last4Digits,
                        Name = creditCard.Name
                    }
                };

                foreach (var month in months)
                {
                    var ts = this.GetReportTransactions(transactions, creditCard, month, year);
                    var total = Math.Round(ts.Sum(t => Math.Round(t.Amount / t.Quotas, 2)), 2);

                    if (month == 1) rm.January = total;
                    else if (month == 2) rm.February = total;
                    else if (month == 3) rm.March = total;
                    else if (month == 4) rm.April = total;
                    else if (month == 5) rm.May = total;
                    else if (month == 6) rm.June = total;
                    else if (month == 7) rm.July = total;
                    else if (month == 8) rm.August = total;
                    else if (month == 9) rm.September = total;
                    else if (month == 10) rm.October = total;
                    else if (month == 11) rm.November = total;
                    else if (month == 12) rm.December = total;
                }

                return rm;
            }).ToList();
        }

        public List<ReportTransactionModel> GetReportTransactions(List<ReportTransactionModel> transactions, CreditCard creditCard, int month, int year)
        {
            var ts = transactions.Where(transaction => transaction.CreditCard.Id.Equals(creditCard.Id)).ToList();
            return ReportUtil.GetTransacctionsToBePaid(ts, creditCard.CutOffDay, month, year);
        }
    }

    public interface IReportService
    {
        List<ReportModel> GetTransactionsReportByYear(int year);
    }
}
