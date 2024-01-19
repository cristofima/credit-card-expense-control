using CreditCardExpenseControl.Core.Models;

namespace CreditCardExpenseControl.Core.Utils
{
    public static class ReportUtil
    {
        public static List<ReportTransactionModel> GetTransacctionsToBePaid(List<ReportTransactionModel> transactions, int cutOffDay, double deferredContributionPercentage, int month, int year)
        {
            List<ReportTransactionModel> newTransactions = new List<ReportTransactionModel>();

            foreach (var transaction in transactions)
            {
                var purchaseDate = transaction.Date;
                int purchaseDay = purchaseDate.Day;
                int purchaseMonth = purchaseDate.Month;
                int purchaseYear = purchaseDate.Year;
                var (firstPaymentMonth, firstPaymentYear) = GetFirstPaymentMonthYear(cutOffDay, purchaseYear, purchaseMonth, purchaseDay);

                if (transaction.Quotas == 1)
                {
                    if (firstPaymentMonth == month && firstPaymentYear == year)
                    {
                        newTransactions.Add(transaction);
                    }
                    else if (transaction.IsRecurringPayment)
                    {
                        var currentDate = new DateTime(year, month, 1);
                        var firstPaymentDate = new DateTime(firstPaymentYear, firstPaymentMonth, 1);

                        if (currentDate >= firstPaymentDate && (!transaction.RecurringPaymentEndDate.HasValue || currentDate <= transaction.RecurringPaymentEndDate.Value))
                        {
                            newTransactions.Add(transaction);
                        }
                    }
                }
                else
                {
                    var firstPaymentDate = new DateTime(firstPaymentYear, firstPaymentMonth, 1);
                    if (transaction.GraceMonths > 0)
                    {
                        firstPaymentDate = firstPaymentDate.AddMonths(transaction.GraceMonths);
                        firstPaymentYear = firstPaymentDate.Year;
                        firstPaymentMonth = firstPaymentDate.Month;
                    }

                    var lastPaymentDate = firstPaymentDate.AddMonths(transaction.Quotas - 1);
                    var currentDate = new DateTime(year, month, 1);

                    if (currentDate >= firstPaymentDate && currentDate <= lastPaymentDate)
                    {
                        if (firstPaymentYear == year && firstPaymentMonth == month)
                        {
                            var cloneTransaction = transaction.Clone() as ReportTransactionModel;
                            cloneTransaction.AproxMonthlyQuota = transaction.AproxMonthlyQuota + (transaction.Amount * deferredContributionPercentage / 100);

                            newTransactions.Add(cloneTransaction);
                        }
                        else
                        {
                            newTransactions.Add(transaction);
                        }
                    }
                }
            }

            return newTransactions;
        }

        public static (int, int) GetFirstPaymentMonthYear(int cutOffDay, int purchaseYear, int purchaseMonth, int purchaseDay)
        {
            int lastDayOfTheMonth = DateTime.DaysInMonth(purchaseYear, purchaseMonth);
            int firstPaymentMonth;
            int firstPaymentYear;

            if (purchaseDay <= cutOffDay)
            {
                if (cutOffDay + 15 <= lastDayOfTheMonth)
                {
                    firstPaymentMonth = purchaseMonth;
                    firstPaymentYear = purchaseYear;
                }
                else
                {
                    if (purchaseMonth == 12)
                    {
                        firstPaymentMonth = 1;
                        firstPaymentYear = purchaseYear + 1;
                    }
                    else
                    {
                        firstPaymentMonth = purchaseMonth + 1;
                        firstPaymentYear = purchaseYear;
                    }
                }
            }
            else
            {
                if (cutOffDay + 15 <= lastDayOfTheMonth)
                {
                    firstPaymentMonth = purchaseMonth + 1;
                }
                else
                {
                    firstPaymentMonth = purchaseMonth + 2;
                }

                if (firstPaymentMonth > 12)
                {
                    firstPaymentMonth -= 12;
                    firstPaymentYear = purchaseYear + 1;
                }
                else
                {
                    firstPaymentYear = purchaseYear;
                }
            }

            return (firstPaymentMonth, firstPaymentYear);
        }
    }
}