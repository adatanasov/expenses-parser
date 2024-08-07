using System;
using System.Text;
using System.Text.RegularExpressions;

namespace expenses_parser
{
    public class RevolutStatementParser : IParser
    {
        private CategoryChooser categoryChooser;

        public RevolutStatementParser(CategoryChooser categoryChooser)
        {
            this.categoryChooser = categoryChooser;
        }

        public string ParseText(string[] lines)
        {
            StringBuilder sb = new StringBuilder("Date,Category,Amount,Note");
            sb.AppendLine();
            double fees = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                line = Regex.Replace(line, "(\"[^\",]+),([^\"]+\")", "$1$2");
                string[] columns = line.Split(",", StringSplitOptions.None);

                double currentFee = double.Parse(columns[6].Trim());
                fees -= currentFee;

                string date = columns[2].Trim();
                string amount = columns[5].Trim();
                string note = string.Join(' ', columns[0].Trim(), columns[1].Trim(), columns[4].Trim());
                string category = this.categoryChooser.GetCategory(note);

                sb.AppendLine($"{date},{category},{amount},\"{note}\"");
            }

            sb.AppendLine($"{DateTime.Now},Bank Tax,{fees},");

            return sb.ToString();
        }
    }
}