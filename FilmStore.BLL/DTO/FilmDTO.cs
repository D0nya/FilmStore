using System.Collections.Generic;

namespace FilmStore.BLL.DTO
{
  public class FilmDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public decimal Price { get; set; }
    public float Rate { get; set; }
    public int ProducerId { get; set; }
    public ProducerDTO Producer { get; set; }

    public List<int> CountriesId { get; set; }
    public List<CountryDTO> Countries { get; set; }

    public List<int> GenresId { get; set; }
    public List<GenreDTO> Genres { get; set; }
    public List<PurchaseDTO> Purchases { get; set; }
  }
}
