using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.Models
{
  public class Purchase
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public virtual Film Film { get; set; }
    public virtual Customer Customer { get; set; }
  }
}
