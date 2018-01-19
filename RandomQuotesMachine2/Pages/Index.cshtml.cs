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

        public async Task<IActionResult> OnGetAsync()
        {
            var rndGen = new Random();
            QuoteList = await _db.Quotes.AsNoTracking().ToListAsync();
            int random = rndGen.Next(0, QuoteList.Count);
            Quote = QuoteList.ElementAt(random);

            return Page();
        }
    }
}
