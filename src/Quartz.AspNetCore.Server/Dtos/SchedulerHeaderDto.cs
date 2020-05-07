namespace Quartz.AspNetCore.Server.Dtos
{
    public class SchedulerHeaderDto
    {
        public SchedulerHeaderDto(IScheduler scheduler)
        {
            Name = scheduler.SchedulerName;
            SchedulerInstanceId = scheduler.SchedulerInstanceId;
            Status = TranslateStatus(scheduler);
        }

        public string Name { get; }
        public string SchedulerInstanceId { get; }
        public SchedulerStatus Status { get; }

        internal static SchedulerStatus TranslateStatus(IScheduler scheduler)
        {
            if (scheduler.IsShutdown) return SchedulerStatus.Shutdown;
            if (scheduler.InStandbyMode) return SchedulerStatus.Standby;
            if (scheduler.IsStarted) return SchedulerStatus.Running;
            return SchedulerStatus.Unknown;
        }
    }
}