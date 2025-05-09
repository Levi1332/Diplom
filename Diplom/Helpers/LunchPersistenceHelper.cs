using System;
using System.IO;
using Newtonsoft.Json;

namespace Diplom.Helpers
{
    public static class LunchPersistenceHelper
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lunch_day.json");

        public static void SaveLunchDay(DateTime date)
        {
            var data = new { Date = date.ToString("yyyy-MM-dd") };
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(data));
        }

        public static DateTime LoadLunchDay()
        {
            if (!File.Exists(FilePath))
                return DateTime.MinValue;

            try
            {
                var json = File.ReadAllText(FilePath);
                var data = JsonConvert.DeserializeObject<LunchDayData>(json);
                return DateTime.TryParse(data?.Date, out DateTime parsed) ? parsed : DateTime.MinValue;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        private class LunchDayData
        {
            public string Date { get; set; }
        }
    }
}
