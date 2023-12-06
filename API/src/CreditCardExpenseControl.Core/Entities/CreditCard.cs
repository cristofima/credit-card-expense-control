using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardExpenseControl.Core.Entities
{
    public class CreditCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Brand { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string Last4Digits { get; set; }
        public int CutOffDay { get; set; }
        public double DeferredContributionPercentage { get; set; }

        public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
    }
}
