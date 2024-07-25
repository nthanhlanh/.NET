using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoPizza.Models;

[Table("MyEntities1")]
public class MyEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public String? Name { get; set; }

    [Column("age")]
     public int Age { get; set; }
}