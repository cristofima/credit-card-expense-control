using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardExpenseControl.Core.Entities
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Quotas { get; set; }
        public int GraceMonths { get; set; }
        public bool IsRecurringPayment { get; set; }
        public DateTime? RecurringPaymentEndDate { get; set; }

        public string CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
