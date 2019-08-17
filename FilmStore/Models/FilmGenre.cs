namespace FilmStore.Models
{
  public class FilmGenre
  {
    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; }
    public int FilmId { get; set; }
    public virtual Film Film { get; set; }
  }
}
