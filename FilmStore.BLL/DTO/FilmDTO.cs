using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FilmStore.BLL.DTO
{
  public class FilmDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public decimal Price { get; set; }
    public float Rate { get; set; }
    public int QuantityInStock { get; set; }

    public int ProducerId { get; set; }
    public ProducerDTO Producer { get; set; }

    public List<int> CountriesId { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public List<CountryDTO> Countries { get; set; }

    public List<int> GenresId { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public List<GenreDTO> Genres { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public List<PurchaseDTO> Purchases { get; set; }
  }
}
