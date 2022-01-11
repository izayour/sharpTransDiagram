using System.Collections.Generic;

namespace WebApp.Domain.Models
{
    public class Hub
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; }
    }
}