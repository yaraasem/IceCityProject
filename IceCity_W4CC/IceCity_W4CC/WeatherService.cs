using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace IceCity_W4CC
{
    public class WeatherService
    {
        public async Task<List<DailyUsage>> FetchLastMonthWeatherAsync()
        {
            List<DailyUsage> usages = new List<DailyUsage>();

            DateTime now = DateTime.UtcNow;
            DateTime start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime end = new DateTime(now.Year, now.Month, 1).AddDays(-1);

            string url = "https://archive-api.open-meteo.com/v1/archive?" +
                         "latitude=31.0409&longitude=31.3785" +
                         "&start_date=" + start.ToString("yyyy-MM-dd") +
                         "&end_date=" + end.ToString("yyyy-MM-dd") +
                         "&daily=temperature_2m_max,temperature_2m_min,precipitation_sum";

            Console.WriteLine("[WeatherService] Fetching: " +
                start.ToString("yyyy-MM-dd") + " to " + end.ToString("yyyy-MM-dd"));

            using (HttpClient httpClient = new HttpClient())
            {
                string response = await httpClient.GetStringAsync(url);

                using (JsonDocument json = JsonDocument.Parse(response))
                {
                    JsonElement daily = json.RootElement.GetProperty("daily");

                    JsonElement.ArrayEnumerator dates = daily.GetProperty("time").EnumerateArray();
                    JsonElement.ArrayEnumerator maxTemps = daily.GetProperty("temperature_2m_max").EnumerateArray();
                    JsonElement.ArrayEnumerator minTemps = daily.GetProperty("temperature_2m_min").EnumerateArray();
                    JsonElement.ArrayEnumerator rain = daily.GetProperty("precipitation_sum").EnumerateArray();

                    while (dates.MoveNext() && maxTemps.MoveNext() &&
                           minTemps.MoveNext() && rain.MoveNext())
                    {
                        string dateStr = dates.Current.GetString();
                        double maxT = maxTemps.Current.GetDouble();
                        double minT = minTemps.Current.GetDouble();
                        double rainMM = rain.Current.GetDouble();

                        Console.WriteLine("  " + dateStr +
                            " | Max:" + maxT + "C" +
                            " | Min:" + minT + "C" +
                            " | Rain:" + rainMM + "mm");

                        DateTime parsedDate;
                        //TryParse: dateStr = "2026-02-01" — string جاية من الـ JSON
                        // بنحوّلها لـ DateTime
                        //out: معناها * *"حط الناتج في المتغير ده"
                        // parsedData بدل ما الدالة ترجع قيمة عادية، بتحط النتيجة مباشرة في 
                        if (!DateTime.TryParse(dateStr, out parsedDate)) continue;


                        double heaterValue = Math.Max(1, 30 - maxT) * 50;
                        double workHours = rainMM > 5 ? 10 : (maxT < 10 ? 12 : 6);
                        if (workHours > 0 && workHours <= 24 && heaterValue > 0)
                            usages.Add(new DailyUsage(parsedDate, workHours, heaterValue));
                    }
                }
            }

            Console.WriteLine("[WeatherService] Loaded " + usages.Count + " records.");
            return usages;
        }

    }
    /*
 عندك عملية بتاخد وقت؟  ──► استخدم async + await                          
البرنامج مش هيتعطّل
المستخدم مش هيحس بحاجة
الكود أنظف وأوضح

*/
}

