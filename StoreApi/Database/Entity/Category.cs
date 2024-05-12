using System.ComponentModel.DataAnnotations;

namespace StoreApi.Database.Entity
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
