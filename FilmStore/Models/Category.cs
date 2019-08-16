using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.Models
{
  public class Category
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<FilmCategory> Films { get; set; }
  }
}
