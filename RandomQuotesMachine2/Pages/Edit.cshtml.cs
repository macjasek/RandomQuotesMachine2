using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RandomQuotesMachine2.Models;
using System;
using System.Threading.Tasks;

namespace RandomQuotesMachine2.Pages
{
    public class EditModel : PageModel
    {
        private readonly QuotesDbContext _db;

        public EditModel(QuotesDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Quotes Quotes { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            Quotes = await _db.Quotes.FindAsync(id);

            if (Quotes == null)
            {
                return RedirectToPage("/QuotesList");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Attach(Quotes).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Quote {Quotes.Id} not found!", e);
            }

            return RedirectToPage("/QuotesList");

        }
    }
}