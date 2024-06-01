using System.Collections;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace DaylioParser.Shell
{
    internal class DaylioShellCommands : IEnumerable<Command>
    {
        private string[]? _args;
        private RootCommand? _rootCommand;
        private List<Command> _commands = new List<Command>();

        public IEnumerable<Command> Commands => _commands;
        public event EventHandler<DaylioShellEventArgs<string>>? GetSummary;
        public event EventHandler<DaylioShellEventArgs<int, string>>? SetDaylioFilePath;
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
            Option fileOption = new Option<string>("--file", "File path of a Daylio CSV file.");
            fileOption.IsRequired = true;
            summaryCommand.AddOption(fileOption);

            DaylioShellEventArgs<string> summaryEventArgs = new DaylioShellEventArgs<string>();
            DaylioShellEventArgs<int, string> setFileEventArgs;

            summaryCommand.Handler = CommandHandler.Create<string>((file) =>
            {
                setFileEventArgs = new DaylioShellEventArgs<int, string>(new string[] { file });
                SetDaylioFilePath?.Invoke(this, setFileEventArgs);
                GetSummary?.Invoke(this, summaryEventArgs);
                Console.WriteLine(summaryEventArgs.Result);
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
