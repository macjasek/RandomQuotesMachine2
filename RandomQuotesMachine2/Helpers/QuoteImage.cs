using RandomQuotesMachine2.Models;
using System.Collections.Generic;
using System.Drawing;

namespace RandomQuotesMachine2.Helpers
{
    public class QuoteImage
    {

        public static void Create(Quotes quote, string fontName, int fontSize, float maximumTextSize)
        {
            Font font = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            string[] words = quote.Qoute.Split(' ');

            var textRows = CalculateNumberOfRows(words, font, maximumTextSize);

            var img = new Bitmap(800, 416);
            var drawing = Graphics.FromImage(img);
            var backColor = new Color();
            backColor = Color.FromArgb(255, 255, 255);
            drawing.Clear(backColor);

            Color textColor = Color.Black;

            var totalQuoteSize = CalculateTextSize(quote.Qoute, font);

            float quoteTextRowHeight = totalQuoteSize.Height * 1.25f;
            float quoteRectangleHeight = quoteTextRowHeight * textRows;

            float imageMiddleHeight = img.Height / 2;
            float quoteRectangleMiddleHeight = quoteRectangleHeight / 2;

            float quoteRectangleY = imageMiddleHeight + quoteRectangleMiddleHeight;

            

            var quoteLines = SplitQuoteOnLines(words, font, maximumTextSize);


            img.Dispose();
            drawing.Dispose();
        }

        private static List<string> SplitQuoteOnLines(string[] words, Font font, float maximumTextSize)
        {
            var quoteLines = new List<string>();

            

            return quoteLines;
        }

        private static int CalculateNumberOfRows(string[] words, Font font, float maximumTextSize)
        {
            int numberOfRows = 1;

            string tempQuote = "";

            foreach (string word in words)
            {
                if (tempQuote == "")
                {
                    tempQuote = word;
                }
                else
                {
                    tempQuote = $"{tempQuote} {word}";
                }

                SizeF textSize = CalculateTextSize(tempQuote, font);

                if (textSize.Width >= maximumTextSize)
                {
                    numberOfRows++;
                    tempQuote = "";
                }
            }


            return numberOfRows;
        }

        private static SizeF CalculateTextSize(string text, Font font)
        {
            Image image = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(image);

            SizeF textSize = drawing.MeasureString(text, font);

            image.Dispose();
            drawing.Dispose();

            return textSize;
        }

        
    }
}
