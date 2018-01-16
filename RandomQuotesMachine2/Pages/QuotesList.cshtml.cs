using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RandomQuotesMachine2.Models;

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

    }
}