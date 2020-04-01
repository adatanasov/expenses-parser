using System;
using System.Text;
using System.Text.RegularExpressions;

namespace expenses_parser
{
    public class SpendingTrackerStatementParser : IParser
    {
        private CategoryChooser categoryChooser;

        public SpendingTrackerStatementParser(CategoryChooser categoryChooser)
        {
            this.categoryChooser = categoryChooser;
        }

        public string ParseText(string[] lines)
        {
            StringBuilder sb = new StringBuilder("Date,Category,Amount,Note");
            sb.AppendLine();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(",", StringSplitOptions.None);

                string date = columns[0].Replace("\"", string.Empty).Trim();
                string category = columns[1].Replace("\"", string.Empty).Trim();
                string amount = columns[2].Replace("\"", string.Empty).Trim();
                string note = columns[3].Replace("\"", string.Empty).Trim();

                sb.AppendLine($"{date},{category},{amount},{note}");
            }

            return sb.ToString();
        }
    }
}