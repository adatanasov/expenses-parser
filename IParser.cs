namespace expenses_parser
{
    public interface IParser
    {
        string ParseText(string[] lines);
    }
}