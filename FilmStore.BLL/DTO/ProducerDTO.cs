using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FilmStore.BLL.DTO
{
  public class ProducerDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public List<FilmDTO> Films { get; set; }
  }
}
