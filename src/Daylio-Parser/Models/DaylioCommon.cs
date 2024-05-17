﻿namespace Daylio_Parser.Models
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

        public static List<string> Activities = new List<string>();

        public static void UpsertActivity(IEnumerable<string> activities)
        {
            foreach (string activity in activities)
            {
                if (!Activities.Contains(activity))
                {
                    Activities.Add(activity);
                }
            }
        }
    }
}
