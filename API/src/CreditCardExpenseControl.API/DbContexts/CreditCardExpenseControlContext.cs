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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Brand)
                    .HasColumnType("nvarchar(25)")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(30)")
                    .IsRequired();

                entity.Property(e => e.Last4Digits)
                    .HasColumnType("nchar(4)")
                    .IsRequired();

                entity.Property(e => e.DeferredContributionPercentage).HasDefaultValue(0.0);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(d => d.CreditCard);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(7, 2)")
                    .IsRequired();

                entity.Property(e => e.CashAdvanceFee)
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnType("nvarchar(40)")
                    .IsRequired();

                entity.Property(e => e.Quotas)
                    .HasDefaultValue(1)
                    .IsRequired();

                entity.Property(e => e.GraceMonths)
                    .HasDefaultValue(0)
                    .IsRequired();

                entity.Property(e => e.IsRecurringPayment)
                    .HasDefaultValue(false)
                    .IsRequired();
                
                entity.Property(e => e.IsCashAdvance)
                    .HasDefaultValue(false)
                    .IsRequired();

                entity.Property(e => e.TotalAmountTransaction)
                    .HasColumnType("decimal(7, 2)");

                entity.Property(e => e.RecurringPaymentEndDate)
                    .HasColumnType("date");
            });
        }
    }
}
