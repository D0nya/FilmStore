using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStore.DAL.Entities
{
  public class News
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public string ImagePath { get; set; }
    public DateTime Date { get; set; }
  }
}
