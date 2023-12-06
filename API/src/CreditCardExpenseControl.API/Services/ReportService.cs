using CreditCardExpenseControl.API.DbContexts;
using CreditCardExpenseControl.Core.Entities;
using CreditCardExpenseControl.Core.Models;
using CreditCardExpenseControl.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CreditCardExpenseControl.API.Services
{
    public class ReportService : IReportService
    {
        private readonly CreditCardExpenseControlContext _context;
        private readonly IMemoryCache _memoryCache;

        public ReportService(CreditCardExpenseControlContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public List<ReportModel> GetReportByYear(int year)
        {
            var creditCards = _context.CreditCards.AsNoTracking().ToList();
            var reportTransactions = this.GetReportTransactions();

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
                    var ts = this.GetReportTransactions(reportTransactions, creditCard, month, year);
                    var total = Math.Round(ts.Sum(t => Math.Round(t.AproxMonthlyQuota, 2)), 2);

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
            return ReportUtil.GetTransacctionsToBePaid(ts, creditCard.CutOffDay, creditCard.DeferredContributionPercentage, month, year);
        }

        public List<ReportTransactionModel> GetReportTransactionsByYearAndMonth(int year, int month)
        {
            var creditCards = _context.CreditCards.AsNoTracking().ToList();
            var reportTransactions = this.GetReportTransactions();
            var rtByYearAndMonth = new List<ReportTransactionModel>();

            foreach (var creditCard in creditCards)
            {
                var ts = this.GetReportTransactions(reportTransactions, creditCard, month, year);
                rtByYearAndMonth.AddRange(ts);
            }

            return rtByYearAndMonth;
        }

        private List<ReportTransactionModel> GetReportTransactions()
        {
            if (!_memoryCache.TryGetValue("REPORT_TRANSACTIONS", out List<ReportTransactionModel> reportTransactions))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                reportTransactions = _context.Transactions.Select(t => new ReportTransactionModel
                {
                    Quotas = t.Quotas,
                    Amount = t.Amount,
                    GraceMonths = t.GraceMonths,
                    Date = t.Date,
                    CreditCard = new CreditCardModel
                    {
                        Brand = t.CreditCard.Brand,
                        Id = t.CreditCard.Id,
                        Last4Digits = t.CreditCard.Last4Digits,
                        Name = t.CreditCard.Name
                    },
                    AproxMonthlyQuota = Math.Round(t.Amount / t.Quotas, 2),
                    Description = t.Description,
                    Id = t.Id,
                    IsRecurringPayment = t.IsRecurringPayment,
                    RecurringPaymentEndDate = t.RecurringPaymentEndDate
                }).AsNoTracking().ToList();

                _memoryCache.Set("REPORT_TRANSACTIONS", reportTransactions, cacheEntryOptions);
            }
            else
            {
                reportTransactions = _memoryCache.Get<List<ReportTransactionModel>>("REPORT_TRANSACTIONS");
            }

            return reportTransactions;
        }
    }

    public interface IReportService
    {
        List<ReportModel> GetReportByYear(int year);
        List<ReportTransactionModel> GetReportTransactionsByYearAndMonth(int year, int month);
    }
}
