using CreditCardExpenseControl.API.Models;

namespace CreditCardExpenseControl.API.Utils
{
    public static class ReportUtil
    {
        public static List<ReportTransactionModel> GetTransacctionsToBePaid(List<ReportTransactionModel> transactions, int cutOffDay, int month, int year)
        {
            List<ReportTransactionModel> newTransactions = new List<ReportTransactionModel>();

            foreach (var transaction in transactions)
            {
                DateTime purchaseDate = transaction.Date;
                int purchaseDay = purchaseDate.Day;
                int purchaseMonth = purchaseDate.Month;
                int purchaseYear = purchaseDate.Year;
                var (firstPaymentMonth, firstPaymentYear) = GetFirstPaymentMonthYear(month, year, cutOffDay, purchaseYear, purchaseMonth, purchaseDay);

                if (transaction.Quotas == 1)
                {
                    if (firstPaymentMonth == month && firstPaymentYear == year)
                    {
                        newTransactions.Add(transaction);
                    }
                }
                else
                {
                    DateTime firstPaymentDate = new DateTime(firstPaymentYear, firstPaymentMonth, 1);
                    DateTime lastPaymentDate = firstPaymentDate.AddMonths(transaction.Quotas - 1);
                    DateTime currentDate = new DateTime(year, month, 1);

                    if (currentDate >= firstPaymentDate && currentDate <= lastPaymentDate)
                    {
                        newTransactions.Add(transaction);
                    }
                }
            }

            return newTransactions;
        }

        private static (int, int) GetFirstPaymentMonthYear(int month, int year, int cutOffDay, int purchaseYear, int purchaseMonth, int purchaseDay)
        {
            int lastDayOfTheMonth = DateTime.DaysInMonth(year, month);
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
                if (cutOffDay + 15 <= lastDayOfTheMonth && purchaseMonth < 12)
                {
                    firstPaymentMonth = purchaseMonth + 1;
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
                        firstPaymentMonth = purchaseMonth + 2;
                        firstPaymentYear = purchaseYear;
                    }
                }
            }

            return (firstPaymentMonth, firstPaymentYear);
        }
    }
}