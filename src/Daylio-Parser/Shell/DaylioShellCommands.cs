using System.Collections;
using System.CommandLine;

namespace Daylio_Parser.Shell
{
    internal class DaylioShellCommands : IEnumerable<Command>
    {
        private string[]? _args;
        private List<Command> _commands = new List<Command>();

        public IEnumerable<Command> Commands => _commands;

        public void Init(params string[] args)
        {
            _args = args;
            BuildCommands();
        }

        private void BuildCommands()
        {
            // Add commands here
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
