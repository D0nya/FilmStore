using FilmStore.BLL.DTO;

namespace FilmStore.WEB.Models.TableLogic
{
  public class SortViewModel
  {
    public SortState NameSort { get; private set; }
    public SortState ProducerSort { get; private set; }
    public SortState YearSort { get; private set; }
    public SortState RateSort { get; set; }
    public SortState PriceSort { get; private set; }
    public SortState QuantitySort { get; private set; }
    public SortState Current { get; private set; }

    public SortViewModel(SortState sortOrder)
    {
      NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
      ProducerSort = sortOrder == SortState.ProducerAsc ? SortState.ProducerDesc : SortState.ProducerAsc;
      YearSort = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
      RateSort = sortOrder == SortState.RateAsc ? SortState.RateDesc : SortState.RateAsc;
      PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
      QuantitySort = sortOrder == SortState.QuantityAsc ? SortState.QuantityDesc : SortState.QuantityAsc;
      Current = sortOrder;
    }

  }
}
