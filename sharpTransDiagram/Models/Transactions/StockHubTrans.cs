using sharpTransDiagram.Common;

namespace sharpTransDiagram.Models.Transactions
{
    public class StockHubTrans : StockTrans
    {
        public int HubId { get; set; }

        public StockHubTrans(string StockAttribute) : base(StockAttribute)
        {
        }

        public override void UpdateTarget(double quantity, string targetType, string targetAttribute, int targetId)
        {
            var targetList = TheDummy.GetList<Target>(targetType);

            int ItemHubId = targetList.Find(i =>
            {
                var itemHub = (ItemHub)i;
                return itemHub.HubId == HubId && itemHub.ItemId == targetId;
            }).GetTargetId();

            base.UpdateTarget(quantity, targetType, targetAttribute, ItemHubId);

            //int index = targetList.FindIndex(i =>
            //{
            //    var itemHub = (ItemHub)i;
            //    return itemHub.HubId == HubId && itemHub.ItemId == targetId;
            //});
            //targetList[index].Update(quantity, targetAttribute);
        }
    }
}