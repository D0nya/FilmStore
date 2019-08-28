namespace FilmStore.DAL.Entities
{
  public class FilmPurchase
  {
    public int FilmId { get; set; }
    public virtual Film Film { get; set; }

    public int PurchaseId { get; set; }
    public virtual Purchase Purchase { get; set; }
  }
}
