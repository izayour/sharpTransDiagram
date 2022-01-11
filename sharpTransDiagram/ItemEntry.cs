namespace WebApp.Domain.Models
{
    public class ItemEntry
    {
        //[Key]
        public int Id { get; set; }

        public int ItemId { get; set; }

        //public Item Item { get; set; }
        public int Qty { get; set; }

        public int Price { get; set; }
    }
}