using System;
using System.Collections.Generic;
using System.Linq;

namespace Quartz.AspNetCore.Server.Dtos
{
    public class ServerDetailsDto
    {
        public ServerDetailsDto(IEnumerable<IScheduler> schedulers)
        {
            Name = Environment.MachineName;
            Address = "localhost";
            Schedulers = schedulers.Select(x => x.SchedulerName).ToList();
        }

        public string Name { get; }
        public string Address { get; }
        public IReadOnlyList<string> Schedulers { get; set; }
    }
}