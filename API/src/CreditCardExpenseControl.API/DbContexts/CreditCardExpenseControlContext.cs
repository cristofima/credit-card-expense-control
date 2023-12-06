using CreditCardExpenseControl.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardExpenseControl.API.DbContexts
{
    public class CreditCardExpenseControlContext : DbContext
    {
        public CreditCardExpenseControlContext(DbContextOptions<CreditCardExpenseControlContext> options) : base(options)
        {
        }

        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
