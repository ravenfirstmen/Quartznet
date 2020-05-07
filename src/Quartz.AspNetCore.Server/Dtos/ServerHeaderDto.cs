namespace Quartz.AspNetCore.Server.Dtos
{
    public class ServerHeaderDto
    {
        public ServerHeaderDto(Api.V1.Server server)
        {
            Name = server.Name;
            Address = server.Address;
        }

        public string Name { get; }
        public string Address { get; }
    }
}