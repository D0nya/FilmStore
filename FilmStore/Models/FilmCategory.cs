namespace FilmStore.Models
{
  public class FilmCategory
  {
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public int FilmId { get; set; }
    public virtual Film Film { get; set; }
  }
}
