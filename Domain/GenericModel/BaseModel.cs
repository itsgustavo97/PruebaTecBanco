using System.ComponentModel.DataAnnotations;

namespace Domain.ModelBase
{
    public abstract class BaseModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public bool Estado { get; set; } = true;
        [Required]
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public DateTime? FechaModificado { get; set; }
    }
}
