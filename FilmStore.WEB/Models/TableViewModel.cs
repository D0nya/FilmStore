using FilmStore.WEB.Models.TableLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.WEB.Models
{
  public class TableViewModel<TSource>
  {
    public IEnumerable<TSource> Items { get; set; }
    public SortViewModel SortViewModel{ get; set; }
    public PageViewModel PageViewModel { get; set; }
  }
}
