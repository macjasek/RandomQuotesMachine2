using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RandomQuotesMachine2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomQuotesMachine2.Pages
{
    public class QuotesListModel : PageModel
    {
        private readonly QuotesDbContext _db;

        public QuotesListModel(QuotesDbContext db)
        {
            _db = db;
        }

        public IList<Quotes> Quotes { get; private set; }

        public async Task OnGetAsync()
        {
            Quotes = await _db.Quotes.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            var quote = await _db.Quotes.FindAsync(id);
            if (quote != null)
            {
                _db.Quotes.Remove(quote);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }

    }
}