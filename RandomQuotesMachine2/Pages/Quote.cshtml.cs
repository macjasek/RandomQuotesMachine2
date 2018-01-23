using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RandomQuotesMachine2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuotesMachine2.Pages
{
    public class QuoteModel : PageModel
    {
        private readonly QuotesDbContext _db;

        public QuoteModel(QuotesDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Quotes Quote { get; set; }
        [BindProperty]
        public Quotes RandomQuote { get; set;}
        [BindProperty]
        public IList<Quotes> QuoteList { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {

            Quote = await _db.Quotes.FindAsync(id);
            Quote.IsSelected = 1;
            _db.Attach(Quote).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Quote {Quote.Id} not found!", e);
            }

            var rndGen = new Random();
            QuoteList = await _db
                .Quotes
                .AsNoTracking()
                .Where(q => q.IsSelected != 1)
                .ToListAsync();
            int random = rndGen.Next(0, QuoteList.Count);
            RandomQuote = QuoteList.ElementAt(random);

            Quote.IsSelected = 0;
            _db.Attach(Quote).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception($"Quote {Quote.Id} not found!", e);
            }

            return Page();
        }
    }
}