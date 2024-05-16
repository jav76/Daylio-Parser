using Daylio_Parser.Shell;

namespace Daylio_Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DaylioShell.Init(args);
            DaylioShell.StartListening();
        }
    }
}
