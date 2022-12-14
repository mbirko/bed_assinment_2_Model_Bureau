using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using model_handin.Data;
using model_handin.Hubs;
using model_handin.Models;

namespace model_handin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ModelDb _context;
        private readonly IHubContext<ExpenseNotification, IExpenseNotification> _expenseNotificationContext;
        public ExpensesController(ModelDb context, IHubContext<ExpenseNotification, IExpenseNotification> expenseNotificationContext )
        {
            _context = context;
            _expenseNotificationContext = expenseNotificationContext;
        }

        // GET: api/Expenses
        [HttpGet]
        private async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        private async Task<IActionResult> PutExpense(long id, Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            var model = _context.Models.FirstOrDefault(x => x.ModelId == expense.ModelId);
            if (model == null)
            {
                return NotFound();
            }

            var job = _context.Jobs.FirstOrDefault(x => x.JobId == expense.JobId);
            if (job == null)
            {
                return NotFound();
            }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            await _expenseNotificationContext.Clients.All.Notification($"New Expense added. Amount: {expense.amount}, Date: {expense.Date}");
            // this demands that the GetExpense endpoint is public, 
            // becuase of this dependensy, we cannot hide it from
            // the user of the api 
            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        private async Task<IActionResult> DeleteExpense(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(long id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
