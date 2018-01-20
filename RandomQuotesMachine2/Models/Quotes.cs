using System.ComponentModel.DataAnnotations;

namespace RandomQuotesMachine2.Models
{
    public partial class Quotes
    {
        public long Id { get; set; }
        [Required]
        public string Qoute { get; set; }
        [Required]
        public string Author { get; set; }
        public int IsSelected { get; set; }
    }
}
