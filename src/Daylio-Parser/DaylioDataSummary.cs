using Daylio_Parser.Models;
using System.Reflection;
using System.Text;

namespace Daylio_Parser
{
    internal static class DaylioDataSummary
    {

        private static IEnumerable<DaylioCSVDataModel>? _CSVData;

        public static IEnumerable<DaylioCSVDataModel>? CSVData => _CSVData;


        [SummaryProperty]
        public static int? TotalEntries => _CSVData?.Count();

        [SummaryProperty]
        public static int? TotalDays => _CSVData?.Select(x => x.FullDate).Distinct().Count();

        [SummaryProperty]
        public static int DistinctActivities => DaylioCommon.Activities.Count();

        [SummaryProperty]
        public static int TotalActivities => _CSVData?.Sum(x => x.Activities?.Split(' ').Length ?? 0) ?? 0;

        [SummaryProperty]
        public static DaylioCSVDataModel? EarliestEntry => _CSVData?.OrderBy(x => x.FullDate).First();

        [SummaryProperty]
        public static DaylioCSVDataModel? LatestEntry => _CSVData?.OrderBy(x => x.FullDate).Last();

        [SummaryProperty]
        public static int NoteTotalWordCount => _CSVData?.Sum(x => x.Note?.Split(' ').Length ?? 0) ?? 0;


        public static void Init(IEnumerable<DaylioCSVDataModel>? daylioData)
        {
            _CSVData = daylioData;
        }

        public static string GetSummary()
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<PropertyInfo> properties = typeof(DaylioDataSummary).GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(SummaryPropertyAttribute)));

            foreach (PropertyInfo property in properties)
            {
                sb.AppendLine($"{property.Name}: {property.GetValue(null)}");
            }

            return sb.ToString();
        }

    }
}
