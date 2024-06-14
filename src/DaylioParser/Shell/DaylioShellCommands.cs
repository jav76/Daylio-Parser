using System.Collections;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace DaylioParser.Shell
{
    internal class DaylioShellCommands : IEnumerable<Command>
    {
        private RootCommand? _rootCommand;
        private List<Command> _commands = new List<Command>();

        public IEnumerable<Command> Commands => _commands;
        public RootCommand? RootCommand => _rootCommand;

        public DaylioShellCommands()
        {
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
            Option fileOption = new Option<string>("--file", "File path of a Daylio CSV file.");
            fileOption.IsRequired = false;
            summaryCommand.AddOption(fileOption);

            summaryCommand.Handler = CommandHandler.Create<string>((file) =>
            {
                if (!string.IsNullOrWhiteSpace(file))
                {
                    DaylioShell.SetDaylioFilePath(file);
                }
                Console.WriteLine(DaylioShell.GetSummary());
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
