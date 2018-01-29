using RandomQuotesMachine2.Models;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace RandomQuotesMachine2.Helpers
{
    public class QuoteImage
    {

        public static void Create(Quotes quote, string fontName, int fontSize, float maximumTextSize)
        {
            Font font = new Font(fontName, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            string[] words = quote.Qoute.Split(' ');

            var quoteLines = SplitQuoteOnLines(words, font, maximumTextSize);
            var imgWidth = 800;
            var imgHeight = 416;

            var img = new Bitmap(imgWidth, imgHeight);
            var drawing = Graphics.FromImage(img);
            var backColor = new Color();
            backColor = Color.FromArgb(86, 86, 86);
            drawing.Clear(backColor);

            Color textColor = Color.White;
            Brush textBrush = new SolidBrush(textColor);

            drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            float x, y;
            y = 0f;

            var quoteHeight = CalculateTextSize(quote.Qoute, font).Height;

            for (int i = 0; i < quoteLines.Count; i++)
            {
                var quoteLinesWidth = CalculateTextSize(quoteLines[i], font).Width;
                x = (imgWidth - quoteLinesWidth) / 2;
                y = (imgHeight / 2) - ((quoteHeight * quoteLines.Count * 0.5f) - (i * quoteHeight));
                drawing.DrawString(quoteLines[i], font, textBrush, x, y);
            }

            Font fontAuthor = new Font(fontName, (int)fontSize * 0.75f, FontStyle.Italic, GraphicsUnit.Pixel);
            var authorLineWidth = CalculateTextSize(quote.Author, fontAuthor).Width;
            x = (img.Width - authorLineWidth) / 2;
            y = y + quoteHeight * 2;
            drawing.DrawString(quote.Author, fontAuthor, textBrush, x, y);

            SaveImageToFile(quote, img, drawing);
            DisposeAssets(font, img, drawing, textBrush, fontAuthor);
        }

        private static void DisposeAssets(Font font, Bitmap img, Graphics drawing, Brush textBrush, Font fontAuthor)
        {
            fontAuthor.Dispose();
            font.Dispose();
            img.Dispose();
            drawing.Dispose();
            textBrush.Dispose();
        }

        private static void SaveImageToFile(Quotes quote, Bitmap img, Graphics drawing)
        {
            drawing.Save();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{quote.Id}-q.png");
            img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        private static List<string> SplitQuoteOnLines(string[] words, Font font, float maximumTextSize)
        {
            var quoteLines = new List<string>();

            string tempQuote = "";
            var tempQuoteBefore = "";

            foreach (string word in words)
            {
                tempQuote = tempQuote == "" ? word : $"{tempQuote} {word}";
                SizeF textSize = CalculateTextSize(tempQuote, font);

                if (textSize.Width >= maximumTextSize)
                {
                    quoteLines.Add(tempQuoteBefore);
                    tempQuote = word;
                }
                tempQuoteBefore = tempQuote;
            }

            quoteLines.Add(tempQuoteBefore);

            return quoteLines;
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
