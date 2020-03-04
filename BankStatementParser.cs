using System;
using System.Text;
using System.Text.RegularExpressions;

namespace expenses_parser
{
    public class BankStatementParser : IParser
    {
        private readonly string stopText = " PAN*";
        private CategoryChooser categoryChooser;

        public BankStatementParser(CategoryChooser categoryChooser)
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
                string[] columns = line.Split("|", StringSplitOptions.None);

                string date = columns[0];

                string minus = columns[3] == "D" ? "-" : string.Empty;
                string amount = $"{minus}{columns[2].Replace(",", string.Empty)}";

                string note = string.Empty;
                for (int j = 4; j < columns.Length - 1; j++)
                {                   
                    string current = $" {columns[j].Trim()}";
                    current = Regex.Replace(current, @"\s+", " ");
                    bool shouldStop = false;
                    if (current.Contains(this.stopText))
                    {
                        current = current.Substring(0, current.IndexOf(this.stopText));
                        shouldStop = true;
                    }

                    note += current;

                    if (shouldStop)
                    {
                        break;
                    }
                }

                string category = this.categoryChooser.GetCategory(note);

                sb.AppendLine($"{date},{category},{amount},\"{note.Trim()}\"");
            }

            return sb.ToString();
        }
    }
}