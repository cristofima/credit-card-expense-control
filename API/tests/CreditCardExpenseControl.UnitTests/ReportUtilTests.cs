using CreditCardExpenseControl.Core.Utils;

namespace CreditCardExpenseControl.UnitTests
{
    public class ReportUtilTests
    {
        [Theory]
        [InlineData(23, 2023, 11, 24, 1, 2024)]
        [InlineData(31, 2023, 12, 20, 1, 2024)]
        [InlineData(31, 2023, 12, 5, 1, 2024)]
        [InlineData(31, 2023, 12, 25, 1, 2024)]
        [InlineData(3, 2023, 12, 12, 1, 2024)]
        [InlineData(3, 2023, 12, 4, 1, 2024)]
        public void GetFirstPaymentMonthYear_PayNextMonth(int cutOffDay, int purchaseYear, int purchaseMonth, int purchaseDay, int firstPaymentMonthExpected, int firstPaymentYearExpected)
        {
            var(firstPaymentMonth, firstPaymentYear) = ReportUtil.GetFirstPaymentMonthYear(cutOffDay, purchaseYear, purchaseMonth, purchaseDay);

            Assert.Equal(firstPaymentYearExpected, firstPaymentYear);
            Assert.Equal(firstPaymentMonthExpected, firstPaymentMonth);
        }

        [Theory]
        [InlineData(19, 2023, 12, 20, 2, 2024)]
        [InlineData(23, 2023, 12, 31, 2, 2024)]
        public void GetFirstPaymentMonthYear_PayInTwoMonths(int cutOffDay, int purchaseYear, int purchaseMonth, int purchaseDay, int firstPaymentMonthExpected, int firstPaymentYearExpected)
        {
            var (firstPaymentMonth, firstPaymentYear) = ReportUtil.GetFirstPaymentMonthYear(cutOffDay, purchaseYear, purchaseMonth, purchaseDay);

            Assert.Equal(firstPaymentYearExpected, firstPaymentYear);
            Assert.Equal(firstPaymentMonthExpected, firstPaymentMonth);
        }
    }
}
