using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace expenses_parser
{
    public class Program
    {
        private readonly static string StopText = " PAN*";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] lines = File.ReadAllLines(args[1], Encoding.UTF8);

            CategoryChooser categoryChooser = new CategoryChooser((Type)int.Parse(args[0]));
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
                    if (current.Contains(Program.StopText))
                    {
                        current = current.Substring(0, current.IndexOf(Program.StopText));
                        shouldStop = true;
                    }

                    note += current;

                    if (shouldStop)
                    {
                        break;
                    }
                }

                string category = categoryChooser.GetCategory(note);

                sb.AppendLine($"{date},{category},{amount},\"{note.Trim()}\"");
            }

            File.WriteAllText($"result-{Guid.NewGuid().ToString()}.csv", sb.ToString(), Encoding.UTF8);
            System.Console.WriteLine("Done!");
        }
    }
}