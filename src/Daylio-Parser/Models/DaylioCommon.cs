namespace Daylio_Parser.Models
{
    public static class DaylioCommon
    {
        public enum Mood
        {
            Rad,
            Good,
            Meh,
            Bad,
            Awful
        }

        public static HashSet<string> Activities = new HashSet<string>();

        public static void UpsertActivity(IEnumerable<string> activities)
        {
            foreach (string activity in activities)
            {
                Activities.Add(activity);
            }
        }
    }
}
