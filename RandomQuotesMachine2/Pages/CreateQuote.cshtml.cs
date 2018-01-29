using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RandomQuotesMachine2.Helpers;
using RandomQuotesMachine2.Models;
using System.Threading.Tasks;

namespace RandomQuotesMachine2.Pages
{
    public class CreateQuoteModel : PageModel
    {
        private readonly QuotesDbContext _db;

        public CreateQuoteModel(QuotesDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Quotes Quotes { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Quotes.Add(Quotes);

            await _db.SaveChangesAsync();
            QuoteImage.Create(Quotes, "Helvetica", 40, 780f);

            return RedirectToPage("/Quote",new { id = Quotes.Id});
        }
    }
}