using DaylioParser.Models;
using DaylioParser.Repo;
using DaylioParser.Shell;

namespace DaylioParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DaylioShell.Init(args);
            DaylioShell.StartListening();

            DaylioFileAccess fileAccess = new DaylioFileAccess(@"C:\Users\jav26\git\Daylio-Parser\daylio_export_2024_05_16.csv");
            DaylioDataRepo daylioDataRepo = new DaylioDataRepo(fileAccess);

            DaylioDataSummary dataSummary = new DaylioDataSummary(daylioDataRepo);
            string summary = dataSummary.GetSummary();

            if (daylioDataRepo.CSVData == null)
            {
                return;
            }

            foreach (DaylioCSVDataModel line in daylioDataRepo.CSVData)
            {
                Console.WriteLine(line.ToString());
                Console.WriteLine(summary);
            }
        }
    }
}
