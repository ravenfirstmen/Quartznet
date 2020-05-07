using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Quartz.AspNetCore.Server.Dtos;
using Quartz.Impl;

namespace Quartz.AspNetCore.Server.Api.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/schedulers/{schedulerName}/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly ILogger<CalendarsController> _logger;

        public CalendarsController(ILogger<CalendarsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IReadOnlyCollection<string>> Calendars(string schedulerName)
        {
            var scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            var calendarNames = await scheduler.GetCalendarNames().ConfigureAwait(false);

            return calendarNames;
        }

        [HttpGet]
        [Route("{calendarName}")]
        public async Task<CalendarDetailDto> CalendarDetails(string schedulerName, string calendarName)
        {
            var scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            var calendar = await scheduler.GetCalendar(calendarName).ConfigureAwait(false);
            return CalendarDetailDto.Create(calendar);
        }

        [HttpPut]
        [Route("{calendarName}")]
        public async Task AddCalendar(string schedulerName, string calendarName, bool replace, bool updateTriggers)
        {
            var scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            ICalendar calendar = null;
            await scheduler.AddCalendar(calendarName, calendar, replace, updateTriggers).ConfigureAwait(false);
        }

        [HttpDelete]
        [Route("{calendarName}")]
        public async Task DeleteCalendar(string schedulerName, string calendarName)
        {
            var scheduler = await GetScheduler(schedulerName).ConfigureAwait(false);
            await scheduler.DeleteCalendar(calendarName).ConfigureAwait(false);
        }

        private static async Task<IScheduler> GetScheduler(string schedulerName)
        {
            var scheduler = await SchedulerRepository.Instance.Lookup(schedulerName).ConfigureAwait(false);
            if (scheduler == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                throw new KeyNotFoundException($"Scheduler {schedulerName} not found!");
            return scheduler;
        }
    }
}