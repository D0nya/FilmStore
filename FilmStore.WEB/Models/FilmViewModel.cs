using System.Collections.Generic;

namespace FilmStore.WEB.Models
{
  public class FilmViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public decimal Price { get; set; }
    public float Rate { get; set; }
    public ProducerViewModel Producer { get; set; }
    public List<CountryViewModel> Countries { get; set; }
    public List<GenreViewModel> Genres { get; set; }
    public List<PurchaseViewModel> Purchases { get; set; }
  }
}
