using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public int ParentId { get; set; }

    }
}
