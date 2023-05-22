using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Base;

namespace Models.Domain;

public class Workspace : BaseModel
{
    [Key]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    [NotMapped]
    public byte[] Image { get; set; } = null!;
}