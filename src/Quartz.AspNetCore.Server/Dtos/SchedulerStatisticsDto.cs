namespace Quartz.AspNetCore.Server.Dtos
{
    public class SchedulerStatisticsDto
    {
        public SchedulerStatisticsDto(SchedulerMetaData metaData)
        {
            NumberOfJobsExecuted = metaData.NumberOfJobsExecuted;
        }

        public int NumberOfJobsExecuted { get; }
    }
}