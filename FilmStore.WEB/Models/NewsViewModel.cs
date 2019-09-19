using Microsoft.AspNetCore.Http;
using System;

namespace FilmStore.WEB.Models
{
  public class NewsViewModel
  {
    public int Id { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public string ImagePath { get; set; }
    public DateTime Date { get; set; }
    public IFormFile Image { get; set; }
  }
}
