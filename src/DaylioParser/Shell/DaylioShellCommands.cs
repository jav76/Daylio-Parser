using Daylio_Parser;
using System.Collections;
using System.CommandLine;

namespace DaylioParser.Shell
{
    internal class DaylioShellCommands : IEnumerable<Command>
    {
        private string[]? _args;
        private RootCommand? _rootCommand;
        private List<Command> _commands = new List<Command>();

        public IEnumerable<Command> Commands => _commands;
        public event EventHandler<DaylioShellEventArgs<string>>? GetSummary;
        public RootCommand? RootCommand => _rootCommand;

        public DaylioShellCommands(params string[] args)
        {
            _args = args;
            BuildCommands();
        }

        private void BuildCommands()
        {
            _rootCommand = new RootCommand("Daylio Shell");
            BuildSummaryCommand();
        }

        private void BuildSummaryCommand()
        {
            Command summaryCommand = new Command("summary", "Get a summary of Daylio data.");
            DaylioShellEventArgs<string> eventArgs = new DaylioShellEventArgs<string>();

            summaryCommand.SetHandler(() =>
            {
                GetSummary?.Invoke(this, eventArgs);
                Console.WriteLine(eventArgs.Result);
            });

            _rootCommand?.Add(summaryCommand);
            _commands.Add(summaryCommand);
        }

        public IEnumerator<Command> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_commands).GetEnumerator();
        }
    }
}
