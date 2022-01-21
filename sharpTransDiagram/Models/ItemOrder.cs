namespace sharpTransDiagram.Models
{
    public class ItemOrder
    {
        //[Key]
        //public int Id { get; set; }

        public int ItemHubId { get; set; }

        //public Item Item { get; set; }
        public int Qty { get; set; }

        public int Fulfilled { get; set; } = 0;

        public int Price { get; set; }
    }
}