using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApi.Database.Entity
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int FakeStoreId { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal? Rate { get; set; }
        public int? RatingCount { get; set; }
    }
}
