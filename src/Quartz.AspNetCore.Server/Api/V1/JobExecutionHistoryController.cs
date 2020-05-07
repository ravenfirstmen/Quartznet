using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Quartz.AspNetCore.Server.Dtos;
using Quartz.Web.History;

namespace Quartz.AspNetCore.Server.Api.V1
{
    /// <summary>
    ///     Web API endpoint for job history. Requires persistent storage to work with.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/schedulers/{schedulerName}/[controller]")]
    public class JobExecutionHistoryController : Controller
    {
        private readonly ILogger<JobExecutionHistoryController> _logger;

        public JobExecutionHistoryController(ILogger<JobExecutionHistoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<JobHistoryViewModel> SchedulerHistory(string schedulerName)
        {
            var jobHistoryDelegate = DatabaseExecutionHistoryPlugin.Delegate;
            IReadOnlyList<JobHistoryEntryDto> entries = new List<JobHistoryEntryDto>();
            string errorMessage = null;

            try
            {
                entries = await jobHistoryDelegate.SelectJobHistoryEntries(schedulerName).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while retrieving history entries");
                errorMessage = e.Message;
            }

            var model = new JobHistoryViewModel(entries, errorMessage);
            return model;
        }
    }
}