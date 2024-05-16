using System.Collections;
using System.CommandLine;

namespace Daylio_Parser.Shell
{
    internal class DaylioShellCommands : IEnumerable<Command>
    {
        private static string[]? _args;
        private static List<Command> _commands = new List<Command>();

        public static IEnumerable<Command> Commands => _commands;

        public DaylioShellCommands(params string[] args)
        {
            _args = args;
            BuildCommands();
        }

        private static void BuildCommands()
        {
            // Add commands here
        }

        #region IEnumerable<Command> implementation

        IEnumerator<Command> IEnumerable<Command>.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        #endregion
    }
}
