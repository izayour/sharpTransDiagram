namespace WebApp.Domain.Models.Gateways
{
    public class HubGateway : Gateway
    {
        public int HubId { get; set; } = 1;
        public Hub Hub { get; set; }

        public HubGateway(int id)
        {
            this.HubId = id;
        }

        public HubGateway(int hubId, string type, string attribute)
        {
            this.HubId = hubId;
            this.Type = type;
            this.Attribute = attribute;
        }

        public HubGateway(string type, string attribute)
        {
            this.Type = type;
            this.Attribute = attribute;
        }
    }
}