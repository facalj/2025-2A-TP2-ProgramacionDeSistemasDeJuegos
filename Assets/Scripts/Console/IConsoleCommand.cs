using System.Collections.Generic;

public interface IConsoleCommand
{
    string Name { get; }
    string Description { get; }
    IEnumerable<string> Aliases { get; }
    void Execute(string[] args);
}