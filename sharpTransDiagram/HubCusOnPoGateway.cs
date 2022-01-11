namespace WebApp.Domain.Models.Gateways
{
    public class HubCusOnPoGateway : HubGateway
    {
        public HubCusOnPoGateway(int hubId) : base(hubId, "Customers", "OnPO")
        {
            //this.SetUnitOfWork();
        }
    }
}