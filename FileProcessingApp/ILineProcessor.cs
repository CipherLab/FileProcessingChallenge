namespace FileProcessingApp;

public interface ILineProcessor
{
    Task<string> ProcessLineAsync(string line);
    string ProcessLine(string line);
    Task<IEnumerable<string>> ProcessLinesAsync(string[] lines);
    IEnumerable<string> ProcessLines(string[] lines);
}
