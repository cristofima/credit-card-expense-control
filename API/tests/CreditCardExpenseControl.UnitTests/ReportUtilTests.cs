using CreditCardExpenseControl.Core.Utils;

namespace CreditCardExpenseControl.UnitTests
{
    public class ReportUtilTests
    {
        [Theory]
        [InlineData(12, 2023, 23, 2023, 11, 24, 1, 2024)]
        [InlineData(12, 2023, 31, 2023, 12, 20, 1, 2024)]
        [InlineData(12, 2023, 31, 2023, 12, 5, 1, 2024)]
        [InlineData(12, 2023, 31, 2023, 12, 25, 1, 2024)]
        [InlineData(12, 2023, 31, 2023, 12, 18, 1, 2024)]
        public void GetFirstPaymentMonthYear_PayNextYear(int month, int year, int cutOffDay, int purchaseYear, int purchaseMonth, int purchaseDay, int firstPaymentMonthExpected, int firstPaymentYearExpected)
        {
            var(firstPaymentMonth, firstPaymentYear) = ReportUtil.GetFirstPaymentMonthYear(month, year, cutOffDay, purchaseYear, purchaseMonth, purchaseDay);

            Assert.Equal(firstPaymentYearExpected, firstPaymentYear);
            Assert.Equal(firstPaymentMonthExpected, firstPaymentMonth);
        }
    }
}
