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
            StringBuilder sb = new StringBuilder("Date,Category,BGN,Note,EUR");
            sb.AppendLine();
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] columns = line.Split(";", StringSplitOptions.None);
                
                string date = columns[3];
                string euroAmount = columns[7];
                bool isNegative = euroAmount.StartsWith("-");
                string bgAmount = this.GetBGN(columns[9]);
                if (isNegative && !string.IsNullOrEmpty(bgAmount))
                {
                    bgAmount = $"-{bgAmount}";
                }
                string note = $"{this.RemoveQuotes(columns[4])} {this.RemoveQuotes(columns[9])}";
                string category = this.categoryChooser.GetCategory(note);

                sb.AppendLine($"{date},{category},{bgAmount},\"{note}\",{euroAmount}");
            }

            return sb.ToString();
        }

        private string RemoveQuotes(string text)
        {
            text = text.Trim(new char[] {' ', '"'});

            return text;        
        }

        private string GetBGN(string text)
        {
            string result = string.Empty;

            if (text.Contains("BGN ") && text.Contains(" FX Rate"))
            {
                int bgnIndex = text.IndexOf("BGN ") + 4;
                int fxRate = text.IndexOf(" FX Rate");
                int length = fxRate - bgnIndex;

                result = text.Substring(bgnIndex, length);
            }

            return result;
        }
    }
}