﻿namespace CreditCardExpenseControl.Core.Models
{
    public class ReportTransactionModel : ICloneable
    {
        public string Id { get; set; }
        public CreditCardModel CreditCard { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Quotas { get; set; }
        public int GraceMonths { get; set; }
        public double AproxMonthlyQuota { get; set; }
        public bool IsRecurringPayment { get; set; }
        public bool IsCashAdvance { get; set; }
        public double? TotalAmountTransaction { get; set; }
        public double? CashAdvanceFee { get; set; }
        public DateTime? RecurringPaymentEndDate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
