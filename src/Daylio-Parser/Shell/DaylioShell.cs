using System.CommandLine;

namespace Daylio_Parser.Shell
{
    internal static class DaylioShell
    {
        private static Dictionary<Task<int>, CancellationTokenSource> _commandListenerTasks = new Dictionary<Task<int>, CancellationTokenSource>();
        private static DaylioShellCommands? _commands;
        private static string[]? _args;

        public static void Init(params string[] args)
        {
            _args = args;
            _commands = new DaylioShellCommands(_args);
        }

        public static void StartListening()
        {
            if (_commands == null || _args == null)
            {
                return;
            }

            foreach (Command command in _commands)
            {
                CancellationTokenSource newCancellationToken = new CancellationTokenSource();
                _commandListenerTasks.Add 
                (
                    Task.Run(() => command.InvokeAsync(_args), newCancellationToken.Token),
                    newCancellationToken
                );
            }
        }

        public static void StopListening()
        {
            foreach (KeyValuePair<Task<int>, CancellationTokenSource> task in _commandListenerTasks)
            {
                task.Value.Cancel();
                task.Value.Dispose();
                _commandListenerTasks.Remove(task.Key);
            }
        }
    }
}
