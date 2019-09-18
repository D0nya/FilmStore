using FilmStore.WEB.Models.TableLogic;
using System.Collections.Generic;

namespace FilmStore.WEB.Models
{
  public class TableViewModel<TSource>
  {
    public IEnumerable<TSource> Items { get; set; }
    public SortViewModel SortViewModel{ get; set; }
    public PageViewModel PageViewModel { get; set; }
    public SearchViewModel Filter { get; set; }
  }
}
