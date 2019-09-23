using FilmStore.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmStore.BLL.Interfaces
{
  public interface IAdminService 
  {
    IEnumerable<GenreDTO> GetGenres();
    IEnumerable<CountryDTO> GetCountries();
    IEnumerable<ProducerDTO> GetProducers();

    Task SaveFilmAsync(FilmDTO filmDTO);
    Task SavePurchaseAsync(PurchaseDTO purchaseDTO);
    Task ChangeQuantityInStockAsync(FilmDTO filmDTO);
    Task DeleteFilmAsync(int id);
  }
}
