using DaylioData.Models;
using DaylioParser.Repo;
using System.CommandLine;

namespace DaylioParser.Shell
{
    internal static class DaylioShell
    {
        private static KeyValuePair<Task<int>, CancellationTokenSource> _commandListenerTask = new KeyValuePair<Task<int>, CancellationTokenSource>();
        private static DaylioShellCommands? _commands;
        private static string[]? _args;
        private static DaylioDataRepo? _dataRepo;
        private static DaylioDataSummary? _dataSummary;
        private static string _fileLocation = string.Empty;

        public static DaylioShellCommands? Commands => _commands;

        public static string FileLocation
        {
            get => _fileLocation;
            set
            {
                _fileLocation = value;
                _dataRepo = new DaylioDataRepo(new DaylioFileAccess(_fileLocation));
                _dataSummary = new DaylioDataSummary(_dataRepo);
            }
        }

        public static void Init(DaylioDataRepo dataRepo, params string[] args)
        {
            _args = args;
            _commands = new DaylioShellCommands();
            _dataRepo = dataRepo;
            _dataSummary = new DaylioDataSummary(_dataRepo);
        }

        public static void StartListening()
        {
            if (_commands == null || _args == null || _commands?.RootCommand == null)
            {
                throw new InvalidOperationException("Shell has not been initialized.");
            }

            CancellationTokenSource newCancellationToken = new CancellationTokenSource();

            _commandListenerTask = new KeyValuePair<Task<int>, CancellationTokenSource>
            (
                Task.Run(() => _commands.RootCommand.InvokeAsync(_args), newCancellationToken.Token),
                newCancellationToken
            );
        }

        public static void StopListening()
        {
            _commandListenerTask.Value.Cancel();
            _commandListenerTask.Value.Dispose();
            _commandListenerTask.Key.Dispose();
        }

        public static string? GetSummary()
        {
            return _dataSummary?.GetSummary();
        }

        public static void SetDaylioFilePath(string filePath)
        {
            string? newFileLocation = Path.GetFullPath(filePath.Replace("\"", ""));

            if (string.IsNullOrWhiteSpace(newFileLocation))
            {
                throw new ArgumentException("File path cannot be null or empty.");
            }

            FileLocation = newFileLocation;

        }

    }
}
