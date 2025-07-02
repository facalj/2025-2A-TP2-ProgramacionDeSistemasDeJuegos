using System.Collections.Generic;
using System.Linq;

public class HelpCommand : IConsoleCommand
{
    private readonly ConsoleController _console;

    public HelpCommand(ConsoleController console)
        => _console = console;

    public string Name => "help";
    public string Description => "Gives help about a command";
    public IEnumerable<string> Aliases => new[] { "h", "hlp" };

    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            _console.Log("Use: help [command]");
            return;
        }

        var cmd = _console.GetCommand(args[0]);
        if (cmd == null)
        {
            _console.Log($"Command '{args[0]}' not found.");
            return;
        }

        _console.Log($"{cmd.Name}: {cmd.Description}\nAliasses: {string.Join(", ", cmd.Aliases)}");
    }
}