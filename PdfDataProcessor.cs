using System.Text;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace FlatRockInterview
{
    public static class PdfDataProcessor
    {
        public static StringBuilder FilterPdfData(this StringBuilder sb)
        {
            string pattern = @"<div .*?</div>";

            MatchCollection matches = Regex.Matches(sb.ToString(), pattern);
            sb.Clear();

            foreach (Match match in matches)
            {
                sb.AppendLine(match.Value);
            }

            return sb;
        }

        public static StringBuilder GetPdfData(this StringBuilder sb)
        {
            var filePath = Path.GetFullPath
                      (Path.Combine
                      (AppDomain.CurrentDomain.BaseDirectory,
                      "..\\..\\..\\", "file.pdf"));

            using PdfDocument document = PdfDocument.Open(filePath);


            foreach (Page page in document.GetPages().SkipLast(1))
            {
                foreach (Word word in page.GetWords())
                {
                    sb.Append(word.Text + " ");
                }

                sb.AppendLine();
            }

            return sb;
        }
    }
}
