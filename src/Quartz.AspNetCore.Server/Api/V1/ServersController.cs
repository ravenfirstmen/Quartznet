using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Quartz.AspNetCore.Server.Dtos;
using Quartz.Impl;

namespace Quartz.AspNetCore.Server.Api.V1
{
    /// <summary>
    ///     Web API endpoint for scheduler information.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ServersController : Controller
    {
        [HttpGet]
        public IList<ServerHeaderDto> AllServers()
        {
            var servers = ServerRepository.LookupAll();

            return servers.Select(x => new ServerHeaderDto(x)).ToList();
        }

        [HttpGet]
        [Route("{serverName}/details")]
        public async Task<ServerDetailsDto> ServerDetails(string serverName)
        {
            var schedulers = await SchedulerRepository.Instance.LookupAll().ConfigureAwait(false);
            return new ServerDetailsDto(schedulers);
        }
    }
}