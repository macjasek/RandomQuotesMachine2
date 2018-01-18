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
    public class IndexModel : PageModel
    {
        private readonly QuotesDbContext _db;

        public IndexModel(QuotesDbContext db)
        {
            _db = db;
        }

        


        [BindProperty]
        public Quotes Quote { get; set; }
        [BindProperty]
        public IList<Quotes> QuoteList { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            var rndGen = new Random();
            QuoteList = await _db.Quotes.AsNoTracking().ToListAsync();
            int random = rndGen.Next(0, QuoteList.Count);
            id = QuoteList.ElementAt(random).Id;

            Quote = await _db.Quotes.FindAsync(id);
            if (Quote == null)
            {
                
                return RedirectToPage($"/{id}");
            }

            return Page();
        }
    }
}
