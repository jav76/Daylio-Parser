using DaylioParser.Shell;
using System.CommandLine;

namespace DaylioParser
{
    internal class Program : IDisposable
    {
        private bool disposedValue;

        static void Main(string[] args)
        {
            DaylioShell.Init(null, args);
            DaylioShell.StartListening();

            foreach (string arg in args)
            {
                ProcessCommand(arg);
            }

            // The "Shell" will parse the input and invoke the appropriate equivalent CommandLine command.
            while (true)
            {
                string? input = Console.ReadLine()?.TrimEnd();
                ProcessCommand(input);
            }
        }

        private static void ProcessCommand(string? input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                string commandName = input.Split(' ')[0];
                string[]? commandArgs = input.Substring(commandName.Length).Split(' ')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

                Command? command = DaylioShell.Commands?.Where(command => command.Name == commandName)
                    .Distinct()
                    .FirstOrDefault();

                command?.InvokeAsync
                    (
                        commandArgs.Count() > 1
                        ? commandArgs
                        : Array.Empty<string>()
                    );
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DaylioShell.StopListening();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Program()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
