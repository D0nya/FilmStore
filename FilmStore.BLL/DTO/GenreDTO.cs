using System.Collections.Generic;

namespace FilmStore.BLL.DTO
{
  public class GenreDTO
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<FilmDTO> Films { get; set; }
  }
}
