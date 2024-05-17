using CsvHelper;
using Daylio_Parser.Models;
using System.Globalization;

namespace Daylio_Parser.Repo
{
    internal class DaylioFileAccess
    {
        private const string FULL_DATE_HEADER = "full_date";
        private const string DATE_HEADER = "date";
        private const string WEEKDAY_HEADER = "weekday";
        private const string TIME_HEADER = "time";
        private const string MOOD_HEADER = "mood";
        private const string ACTIVITIES_HEADER = "activities";
        private const string NOTE_TITLE_HEADER = "note_title";
        private const string NOTE_HEADER = "note";
        
        private string _filePath = string.Empty;

        public static HashSet<string> CSVHeaders = new HashSet<string>()
        {
            FULL_DATE_HEADER,
            DATE_HEADER,
            WEEKDAY_HEADER,
            TIME_HEADER,
            MOOD_HEADER,
            ACTIVITIES_HEADER,
            NOTE_TITLE_HEADER,
            NOTE_HEADER
        };

        public DaylioFileAccess(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<CSVDataModel>? TryReadFile()
        {
            List<CSVDataModel> CSVData = new List<CSVDataModel>();
            CsvHelper.Configuration.IReaderConfiguration readerConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                IgnoreBlankLines = true,
                TrimOptions = CsvHelper.Configuration.TrimOptions.Trim,
                BadDataFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower().Replace("_", string.Empty) // Conversion from snake_case headers
            };

            try
            {
                using (StreamReader streamReader = new StreamReader(_filePath))
                {
                    using (var CSVReader = new CsvReader(streamReader, readerConfig))
                    {
                        CSVReader.Read();
                        CSVReader.ReadHeader();
                        IEnumerable<CSVDataModel>readHeader = CSVReader.GetRecords<CSVDataModel>();
                        while (CSVReader.Read())
                        {
                            DaylioCommon.UpsertActivity(CSVReader.GetField(ACTIVITIES_HEADER).Split(" | ")); // Split activities by delimiter " | "
                            CSVData.Add(CSVReader.GetRecord<CSVDataModel>());
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return CSVData;
        }

    }
}
