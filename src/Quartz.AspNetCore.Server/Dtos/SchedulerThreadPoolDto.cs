using Quartz.Util;

namespace Quartz.AspNetCore.Server.Dtos
{
    public class SchedulerThreadPoolDto
    {
        public SchedulerThreadPoolDto(SchedulerMetaData metaData)
        {
            Type = metaData.ThreadPoolType.AssemblyQualifiedNameWithoutVersion();
            Size = metaData.ThreadPoolSize;
        }

        public string Type { get; }
        public int Size { get; }
    }
}