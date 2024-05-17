using Daylio_Parser.Models;
using Daylio_Parser.Repo;
using Daylio_Parser.Shell;

namespace Daylio_Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DaylioShell.Init(args);
            DaylioShell.StartListening();

            DaylioFileAccess fileAccess = new DaylioFileAccess(@"C:\Users\jav26\git\Daylio-Parser\daylio_export_2024_05_16.csv");
            IEnumerable<CSVDataModel>? fileData = fileAccess.TryReadFile();

            if (fileData == null)
            {
                return;
            }

            foreach (CSVDataModel line in fileData)
            {
                Console.WriteLine(line.ToString());
            }
        }
    }
}
