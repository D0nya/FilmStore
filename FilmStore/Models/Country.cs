using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.Models
{
  public class Country
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<FilmCountry> Films { get; set; }
  }

  public class FilmCountry
  {
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }

    public int FilmId { get; set; }
    public virtual Film Film { get; set; }
  }
}
