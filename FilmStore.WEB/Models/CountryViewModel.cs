using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FilmStore.WEB.Models
{
  public class CountryViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    [IgnoreDataMember]
    public List<FilmViewModel> Films { get; set; }
  }
}
