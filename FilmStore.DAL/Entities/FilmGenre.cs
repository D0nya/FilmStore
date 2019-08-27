namespace FilmStore.DBL.Entities
{
  public class FilmGenre
  {
    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; }
    public int FilmId { get; set; }
    public virtual Film Film { get; set; }
  }
}
