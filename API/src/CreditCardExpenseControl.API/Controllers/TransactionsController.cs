using CreditCardExpenseControl.API.DbContexts;
using CreditCardExpenseControl.Core.Entities;
using CreditCardExpenseControl.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCardExpenseControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly CreditCardExpenseControlContext _context;

        public TransactionsController(CreditCardExpenseControlContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        // GET: api/Transactions
        [HttpGet]
        [Route("ReportTransaction")]
        public ActionResult<IEnumerable<ReportTransactionModel>> GetReportTransaction()
        {
            return _context.Transactions.Select(t => new ReportTransactionModel
            {
                Quotas = t.Quotas,
                Amount = t.Amount,
                GraceMonths = t.GraceMonths,
                Date = t.Date,
                CreditCard = new CreditCardModel
                {
                    Brand = t.CreditCard.Brand,
                    Id = t.CreditCard.Id,
                    Last4Digits = t.CreditCard.Last4Digits,
                    Name = t.CreditCard.Name
                },
                AproxMonthlyQuota = Math.Round(t.Amount / t.Quotas, 2),
                Description = t.Description,
                Id = t.Id,
                IsRecurringPayment = t.IsRecurringPayment,
                IsCashAdvance = t.IsCashAdvance,
                CashAdvanceFee = t.CashAdvanceFee,
                RecurringPaymentEndDate = t.RecurringPaymentEndDate
            }).AsNoTracking().ToList();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(string id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(string id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }

        [Route("multiple")]
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostMultipleTransaction(List<Transaction> transactions)
        {
            _context.Transactions.AddRange(transactions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactions", null, transactions);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(string id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(string id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}