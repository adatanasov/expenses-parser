using System;
using System.Text;
using System.Text.RegularExpressions;

namespace expenses_parser
{
    public class PayseraStatementParser : IParser
    {
        private CategoryChooser categoryChooser;

        public PayseraStatementParser(CategoryChooser categoryChooser)
        {
            this.categoryChooser = categoryChooser;
        }

        public string ParseText(string[] lines)
        {
            StringBuilder sb = new StringBuilder("Date,Category,EUR,Note");
            sb.AppendLine();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(";", StringSplitOptions.None);
                
                string date = columns[3];
                string euroAmount = columns[7];
                string note = $"{this.RemoveQuotes(columns[4])} {this.RemoveQuotes(columns[9])}";
                string category = this.categoryChooser.GetCategory(note);

                sb.AppendLine($"{date},{category},{euroAmount},\"{note}\"");
            }

            return sb.ToString();
        }

        private string RemoveQuotes(string text)
        {
            text = text.Trim(new char[] {' ', '"'});

            return text;        
        }
    }
}