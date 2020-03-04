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
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(";", StringSplitOptions.None);

                string date = columns[0].Trim();

                string moneyOut = columns[2].Trim();
                string moneyIn = columns[3].Trim();
                string minus = !string.IsNullOrWhiteSpace(moneyOut) ? "-" : string.Empty;
                string money = !string.IsNullOrWhiteSpace(moneyOut) ? moneyOut : moneyIn;
                string amount = $"{minus}{money.Replace(",", string.Empty)}";

                string note = columns[1].Trim();
                for (int j = 8; j < columns.Length; j++)
                {                   
                    string current = $" {columns[j].Trim()}";    
                    note += current;             
                }

                string category = this.categoryChooser.GetCategory(note);

                sb.AppendLine($"{date},{category},{amount},\"{note.Trim()}\"");
            }

            return sb.ToString();
        }
    }
}