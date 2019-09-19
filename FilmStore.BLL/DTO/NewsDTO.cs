using System;

namespace FilmStore.BLL.DTO
{
  public class NewsDTO
  {
    public int Id { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public string ImagePath { get; set; }
    public DateTime Date { get; set; }
  }
}
