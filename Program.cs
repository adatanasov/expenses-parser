using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace expenses_parser
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] lines = File.ReadAllLines(args[1], Encoding.UTF8);
            
            Type parseType = (Type)int.Parse(args[0]);
            CategoryChooser categoryChooser = new CategoryChooser(parseType);
            IParser parser = null;
            switch (parseType)
            {
                case Type.Personal:
                case Type.Common:
                    parser = new BankStatementParser(categoryChooser);
                    break;
                case Type.Revolut:
                    parser = new RevolutStatementParser(categoryChooser);
                    break;
                case Type.SpendingTraker:
                    parser = new SpendingTrackerStatementParser(null);
                    break;
                case Type.Paysera:
                    parser = new PayseraStatementParser(categoryChooser);
                    break;
                default:
                    throw new ArgumentException("Unknown parse type.");
            }
            string result = parser.ParseText(lines);

            File.WriteAllText($"result-{parseType}-{Guid.NewGuid().ToString()}.csv", result, Encoding.UTF8);
            System.Console.WriteLine("Done!");
        }
    }
}