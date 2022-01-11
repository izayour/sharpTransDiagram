namespace WebApp.Domain.Models.Gateways
{
    public class HubOnSOGateway : HubGateway
    {
        public HubOnSOGateway(int hubId) : base(hubId, "Customers", "OnSO")
        {
        }
    }
}