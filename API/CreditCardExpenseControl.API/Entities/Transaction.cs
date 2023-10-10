using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardExpenseControl.API.Entities
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

        public string CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
