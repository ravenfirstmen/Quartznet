using System;

namespace Quartz.AspNetCore.Server.Dtos
{
    public class CurrentlyExecutingJobDto
    {
        public CurrentlyExecutingJobDto(IJobExecutionContext context)
        {
            FireInstanceId = context.FireInstanceId;
            FireTime = context.FireTimeUtc;
            Trigger = new KeyDto(context.Trigger.Key);
            Job = new KeyDto(context.JobDetail.Key);
            JobRunTime = context.JobRunTime;
            RefireCount = context.RefireCount;

            Recovering = context.Recovering;
            if (context.Recovering) RecoveringTrigger = new KeyDto(context.RecoveringTriggerKey);
        }

        public string FireInstanceId { get; }
        public DateTimeOffset? FireTime { get; }
        public KeyDto Trigger { get; }
        public KeyDto Job { get; }
        public TimeSpan JobRunTime { get; }
        public int RefireCount { get; }
        public KeyDto RecoveringTrigger { get; }
        public bool Recovering { get; }
    }
}