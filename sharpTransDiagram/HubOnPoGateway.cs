namespace WebApp.Domain.Models.Gateways
{
    public class HubOnPoGateway : HubGateway
    {
        public HubOnPoGateway(int hubId) : base(hubId, "Items", "OnPO")
        {
        }
    }
}