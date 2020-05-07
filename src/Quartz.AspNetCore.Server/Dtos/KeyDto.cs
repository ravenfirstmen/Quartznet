namespace Quartz.AspNetCore.Server.Dtos
{
    public class KeyDto
    {
        public KeyDto(JobKey jobKey)
        {
            Name = jobKey.Name;
            Group = jobKey.Group;
        }

        public KeyDto(TriggerKey triggerKey)
        {
            Name = triggerKey.Name;
            Group = triggerKey.Group;
        }

        public string Name { get; }
        public string Group { get; }
    }
}