using DaylioData.Models;
using DaylioParser.Shell;
using System.CommandLine;

namespace DaylioParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DaylioData.DaylioData daylioData = new DaylioData.DaylioData(@"C:\Users\jav26\git\Daylio-Parser\daylio_export_2024_06_14.csv");

            DaylioShell.Init(daylioData.DataRepo, args);
            DaylioShell.StartListening();

            DaylioDataSummary dataSummary = daylioData.DataSummary;
            string summary = dataSummary.GetSummary();

            if (daylioData.DataRepo.CSVData == null)
            {
                return;
            }

            foreach (DaylioCSVDataModel line in daylioData.DataRepo.CSVData)
            {
               Console.WriteLine(line.ToString());
            }

            // The "Shell" will parse the input and invoke the appropriate equivalent CommandLine command.
            while (true)
            {
                string? input = Console.ReadLine()?.TrimEnd();

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
        }
    }
}
