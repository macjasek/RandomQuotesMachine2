using RandomQuotesMachine2.Models;
using System.Drawing;

namespace RandomQuotesMachine2.Helpers
{
    public class QuoteImage
    {

        public static void Create(Quotes quote, string fontName, int fontSize, float maximumTextSize)
        {
            Font font = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

            var textRows = CalculateNumberOfRows(quote.Qoute, font, maximumTextSize);

            var img = new Bitmap(800, 416);
            var drawing = Graphics.FromImage(img);
            var backColor = new Color();
            backColor = Color.FromArgb(255, 255, 255);
            drawing.Clear(backColor);

            Color textColor = Color.Black;

            var totalQuoteSize = CalculateTextSize(quote.Qoute, font);

            float quoteTextRowHeight = totalQuoteSize.Height * 1.25f;
            float quoteRectangleHeight = quoteTextRowHeight * textRows;



            for (int i = 0; i < textRows; i++)
            {
                //draw text on image
            }


            img.Dispose();
            drawing.Dispose();
        }

        private static int CalculateNumberOfRows(string quoteText, Font font, float maximumTextSize)
        {
            int numberOfRows = 1;

            string[] words = quoteText.Split(' ');

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
