using sharpTransDiagram.Common;
using sharpTransDiagram.Models;
using sharpTransDiagram.Models.Transactions;
using System;
using System.Collections.Generic;

namespace sharpTransDiagram
{
    public class SO : CompoundTransaction
    {
        public int HubId { get; set; }
        public double FulfillTotal { get; set; } = 0;
        public List<ItemOrder> ItemOrders { get; set; }
        private SoState SoState { get; set; } = SoState.Open;
        public ShippingMethods ShippingMethod { get; set; }
        public int ShippingInfoId { get; set; }

        public SO(DummyData theDummy) : base(theDummy)
        {
        }

        public string GetState()
        {
            return this.SoState.ToString();
        }

        public void CreateTransForItem()
        {
            // create a stockTransaction for each item ordered
            this.SoState = SoState.Open;
            ItemOrders.ForEach(e =>
            {
                StockHubTrans onSoTrans = new StockHubTrans(Constants.OnSo)
                { Id = 1, HubId = this.HubId, Adding = true, TargetId = e.ItemHubId, Quantity = e.Qty, Price = e.Price, TheDummy = this.TheDummy };

                this.Total += onSoTrans.GetAmount();

                this.LeafTransList.Add(onSoTrans);
            });
        }

        // create an account transaction for the customer
        public void CreateAccountTransaction()
        {
            AccountTrans customerTrans = new AccountTrans(Constants.Customer, Constants.OnSo) { Id = 1, Adding = true, TargetId = this.TargetId, Quantity = this.Total, TheDummy = this.TheDummy };
            LeafTransList.Add(customerTrans);
        }

        public void Fulfill(ItemOrder order, int serialOrQty)
        {
            if (order.Qty == order.Fulfilled)
            {
                Console.WriteLine("order already fulfilled");
                return;
            }
            // get the itemhub related to the entry
            var itemhub = (ItemHub)TheDummy.ItemHubs.Find(i =>
            {
                var iHub = (ItemHub)i;
                return iHub.Id == order.ItemHubId;
            });
            if (itemhub != null)
            {
                // calculate the max allowed quantity that can be fulfilled in the itemhub
                int availableQuantity = itemhub.OnHand - itemhub.OnFulfill;
                if (availableQuantity == 0)
                {
                    Console.WriteLine("can't fullfill this order since no items on hand available");
                    return;
                }
                // calculate the required quantity to fulfill the order
                // check for serialization
                if (itemhub.IsSerialized)
                {
                    // update the serial to be unavailable
                    if (this.TheDummy.UpdateSerial(itemhub.ItemId, serialOrQty))
                    {
                        Console.WriteLine(" fulfill success done");
                        // increase the fulfillment of item order by 1
                        order.Fulfilled += 1;
                    }
                    else
                    {
                        Console.WriteLine("failed, no such serial, or serial already token try again ");
                        return;
                    }
                    // apply stock transactions to the items that are fulfilled
                    StockTrans onFulfillTrans = new StockHubTrans(Constants.OnFulfill)
                    { Id = 1, HubId = this.HubId, Adding = true, TargetId = order.ItemHubId, Quantity = 1, Price = order.Price, TheDummy = this.TheDummy };
                    FulfillTotal += onFulfillTrans.GetAmount();
                    onFulfillTrans.Post();
                }
                // non serialized
                else
                {
                    if (serialOrQty > order.Qty)
                    {
                        Console.WriteLine("requested quantity greater than order quantity");
                        return;
                    }
                    var allowedFulfillQty = Math.Min(order.Qty, availableQuantity);
                    Console.WriteLine("enter quantity to fulfill itemHub (" + order.ItemHubId + "), max " + allowedFulfillQty + " allowed.");
                    // check if quantity to fulfill greater than allowed one
                    if (serialOrQty > allowedFulfillQty)
                    {
                        Console.WriteLine("no available quantity to fulfill in itemHub  " + order.ItemHubId);
                        return;
                    }
                    else
                    {
                        order.Fulfilled += serialOrQty;
                    }
                    // apply stock transactions to the items that are fulfilled
                    StockTrans onFulfillTrans = new StockHubTrans(Constants.OnFulfill)
                    { Id = 1, HubId = this.HubId, Adding = true, TargetId = order.ItemHubId, Quantity = serialOrQty, Price = order.Price, TheDummy = this.TheDummy };
                    FulfillTotal += onFulfillTrans.GetAmount();
                    onFulfillTrans.Post();
                }

                // change the status of the So according to total
                if (FulfillTotal == Total)
                {
                    this.SoState = SoState.Fulfilled;
                }
                else
                {
                    this.SoState = SoState.PartialFulfilled;
                }
            }
        }

        public void FulfillAll()
        {
            Console.WriteLine("Fulfilling SO:\n");
            // loop through each itemEntry
            ItemOrders.ForEach(e =>
            {
                Fulfill(e, e.Qty);
            });
        }

        public void ShipPickUpOrder(ItemOrder order)
        {
            // when SO is fulfilled directly create transactions for all item orders
            if (this.SoState == SoState.Fulfilled || this.SoState == SoState.PartialFulfilled)
            {
                var itemhub = (ItemHub)TheDummy.ItemHubs.Find(i =>
                {
                    var iHub = (ItemHub)i;
                    return iHub.Id == order.ItemHubId;
                });

                StockTrans sht1 = new StockTrans(Constants.OnSo)
                { Id = 1, Adding = false, TargetId = order.ItemHubId, Quantity = order.Fulfilled, Price = order.Price, TheDummy = this.TheDummy };
                StockTrans sht2 = new StockTrans(Constants.OnHand)
                { Id = 1, Adding = false, TargetId = order.ItemHubId, Quantity = order.Fulfilled, Price = order.Price, TheDummy = this.TheDummy };
                StockTrans st = new StockHubTrans(Constants.OnFulfill)
                { Id = 1, HubId = this.HubId, Adding = false, TargetId = order.ItemHubId, Quantity = order.Fulfilled, Price = order.Price, TheDummy = this.TheDummy };

                sht1.Post();
                sht2.Post();
                st.Post();
            }
        }

        public void ShipPickUp()
        {
            // when SO is fulfilled directly create transactions for all item entries
            if (this.SoState == SoState.Fulfilled)
            {
                Console.WriteLine("Shiping Picking Up SO:\n");

                ItemOrders.ForEach(order =>
                {
                    ShipPickUpOrder(order);
                });
                this.SoState = SoState.Shipped;
            }
            // when SO is partially fulfilled create transactions for available items
            else if (this.SoState == SoState.PartialFulfilled)
            {
                Console.WriteLine("partial Shiping Picking Up SO:\n");

                ItemOrders.ForEach(order =>
                {
                    ShipPickUpOrder(order);
                });
                // change the state accordingly
                this.SoState = SoState.PartialShipped;
            }
        }

        public void Edit(List<ItemOrder> ItemOrders)
        {
            if (SoState == SoState.Open)
            {
                this.ItemOrders = ItemOrders;
                // clear the old transaction list
                LeafTransList.Clear();
                // create new transactions for the new edited itemEntries
                CreateTransForItem();
                CreateAccountTransaction();
            }
        }

        public void Void()
        {
            if (SoState == SoState.Open)
            {
                // roll back creation transaction
                base.UnPost();
                this.SoState = SoState.Voided;
            }
            if (SoState == SoState.Fulfilled || SoState == SoState.PartialFulfilled)
            {
                // roll back creation transaction
                base.UnPost();
                // roll back any fulfillment
                ItemOrders.ForEach(i =>
                {
                    var trans = new StockHubTrans(Constants.OnFulfill) { TargetId = i.ItemHubId, Adding = false, HubId = this.HubId, Quantity = i.Fulfilled, TheDummy = TheDummy };
                    trans.Post();
                });

                LeafTransList.Clear();
                this.SoState = SoState.Voided;
            }
        }
    }
}