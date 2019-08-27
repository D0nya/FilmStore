using System.Collections.Generic;

namespace FilmStore.WEB.Models
{
  public class CountryViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<FilmViewModel> Films { get; set; }
  }
}
