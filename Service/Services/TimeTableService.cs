using HtmlAgilityPack;
using Service.Interfaces;
using Service.Models.WebScrappingModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Service.Services.TimeTableService;

namespace Service.Services
{
    public class TimeTableService : ITimeTableService
    {
        private readonly HttpClient _httpClient;
        private const string TimeTableUrl = "https://georgianbus.com/page/timetable";

        public TimeTableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TimeTableResponse> GetTimeTableAsync(string city)
        {
            try
            {
                var html = await _httpClient.GetStringAsync(TimeTableUrl);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var timeTableResponse = new TimeTableResponse();
                var processedDates = new HashSet<DateTime>();
                bool isFirstEntry = true; // Add flag for first entry

                string tableId = city switch
                {
                    "city_1" => "table1", // Tbilisi
                    "city_3" => "table3", // Batumi
                    "city_5" => "table5", // Gudauri
                    _ => "table3" // Default
                };

                var timetableRows = doc.DocumentNode.SelectNodes($"//div[@id='{tableId}']//div[contains(@class, 'timetable-row')]");
                if (timetableRows == null) return timeTableResponse;

                foreach (var row in timetableRows)
                {
                    var dateNode = row.SelectSingleNode(".//div[contains(@class, 'column-day')]//span");
                    if (dateNode == null) continue;

                    var date = ParseDate(dateNode.InnerText.Trim());

                    // Skip first entry and then reset the flag
                    if (isFirstEntry)
                    {
                        isFirstEntry = false;
                        continue;
                    }

                    // Skip if we've already processed this date
                    if (processedDates.Contains(date))
                        continue;

                    processedDates.Add(date);

                    var currentEntry = new TimeTableEntry
                    {
                        Date = date,
                        Routes = new List<RouteSchedule>()
                    };

                    var listRows = row.SelectNodes(".//div[contains(@class, 'timetable-list-row')]");
                    if (listRows != null)
                    {
                        foreach (var listRow in listRows)
                        {
                            var departureNode = listRow.SelectSingleNode(".//div[contains(@class, 'column-departure')]//span");
                            var arrivalNode = listRow.SelectSingleNode(".//div[contains(@class, 'column-arrival')]//span");
                            var cityNodes = listRow.SelectNodes(".//div[contains(@class, 'column-city')]//span");

                            if (departureNode != null && arrivalNode != null && cityNodes != null)
                            {
                                var schedule = new RouteSchedule
                                {
                                    DepartureTime = departureNode.InnerText.Trim(),
                                    ArrivalTime = arrivalNode.InnerText.Trim(),
                                    Destinations = cityNodes.Select(n => n.InnerText.Trim().TrimEnd(','))
                                                         .Where(d => !string.IsNullOrWhiteSpace(d))
                                                         .ToList()
                                };
                                currentEntry.Routes.Add(schedule);
                            }
                        }
                    }

                    if (currentEntry.Routes.Any())
                    {
                        timeTableResponse.Entries.Add(currentEntry);
                    }
                }

                DateTime today = DateTime.Today;
                DateTime startTime = today.AddHours(22); // Current day 22:00
                DateTime endTime = today.AddDays(1).AddHours(6).AddMinutes(30); // Following day 06:30

                var filteredResponse = new TimeTableResponse
                {
                    Entries = timeTableResponse.Entries
                                                        .Select(entry =>
                                                        {
                                                            var filteredRoutes = entry.Routes.Where(route =>
                                                            {
                                                                var departureDateTime = ParseDateTime(entry.Date, route.DepartureTime);
                                                                return departureDateTime >= startTime && departureDateTime <= endTime;
                                                            }).ToList();

                                                            return new TimeTableEntry
                                                            {
                                                                Date = entry.Date,
                                                                Routes = filteredRoutes
                                                            };
                                                        })
                                                        .Where(entry => entry.Routes.Any()) // Remove entries with no routes
                                                        .ToList()
                };



                return filteredResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch timetable data", ex);
            }
        }

        private DateTime ParseDate(string dateText)
        {
            try
            {
                // Clean up the text (remove <br /> and extra whitespace)
                dateText = dateText.Replace("<br />", "").Trim();

                // Extract the date part (format: "დღე dd.MM")
                var parts = dateText.Split(' ');
                if (parts.Length < 2) return DateTime.Today;

                var datePart = parts[1]; // Get "dd.MM" part

                if (DateTime.TryParseExact(datePart, "dd.MM",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime result))
                {
                    return new DateTime(DateTime.Today.Year, result.Month, result.Day);
                }

                return DateTime.Today;
            }
            catch
            {
                return DateTime.Today;
            }
        }

        private static DateTime ParseDateTime(DateTime date, string time)
        {
            if (DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
            {
                return new DateTime(date.Year, date.Month, date.Day, parsedTime.Hour, parsedTime.Minute, 0);
            }

            return DateTime.MinValue; // Return a default value if parsing fails
        }
    }
}
