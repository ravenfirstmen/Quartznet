using Quartz.Util;

namespace Quartz.AspNetCore.Server.Dtos
{
    public class SchedulerJobStoreDto
    {
        public SchedulerJobStoreDto(SchedulerMetaData metaData)
        {
            Type = metaData.JobStoreType.AssemblyQualifiedNameWithoutVersion();
            Clustered = metaData.JobStoreClustered;
            Persistent = metaData.JobStoreSupportsPersistence;
        }

        public string Type { get; }
        public bool Clustered { get; }
        public bool Persistent { get; }
    }
}