namespace FilmStore.Models
{
  public class FilmCountry
  {
    public int CountryId { get; set; }
    public virtual Country Country { get; set; }

    public int FilmId { get; set; }
    public virtual Film Film { get; set; }
  }
}
