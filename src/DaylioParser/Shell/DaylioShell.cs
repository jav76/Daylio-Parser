using Daylio_Parser;
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

        public static event EventHandler<DaylioShellEventArgs<string>>? GetSummary;
        public static DaylioShellCommands? Commands => _commands;

        public static void Init(DaylioDataRepo dataRepo, params string[] args)
        {
            _args = args;
            _commands = new DaylioShellCommands(args);
            _dataRepo = dataRepo;
            _dataSummary = new DaylioDataSummary(_dataRepo);
        }

        #region Shell Events

        private static void AddEventListeners()
        {
            if (_commands == null)
            {
                throw new InvalidOperationException("Commands have not been initialized.");
            }

            _commands.GetSummary += GetSummaryHandler;
        }

        private static void RemoveEventListeners()
        {
            if (_commands == null)
            {
                throw new InvalidOperationException("Commands have not been initialized.");
            }

            _commands.GetSummary -= GetSummaryHandler;
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

            AddEventListeners();
        }

        public static void StopListening()
        {
            _commandListenerTask.Value.Cancel();
            _commandListenerTask.Value.Dispose();
            _commandListenerTask.Key.Dispose();
            RemoveEventListeners();
        }

        public static void GetSummaryHandler(object? sender, DaylioShellEventArgs<string> e)
        {
            e.Result = _dataSummary?.GetSummary();
        }

        #endregion

    }
}
