using System;
using System.Collections.Generic;

namespace RandomQuotesMachine2.Models
{
    public partial class Quotes
    {
        public long Id { get; set; }
        public string Qoute { get; set; }
        public string Author { get; set; }
        public int IsSelected { get; set; }
    }
}
