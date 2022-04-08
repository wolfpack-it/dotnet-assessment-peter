using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wolfpack.Data.Database.Entities;

public class Pack
{
    public Pack()
    {
        Wolves = new HashSet<Wolf>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] 
    [MaxLength(64)] 
    public string Name { get; set; } = null!;

    public ICollection<Wolf> Wolves { get; set; }
}