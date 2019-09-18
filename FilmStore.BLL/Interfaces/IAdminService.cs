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

    Task<FilmDTO> GetFilm(int id);

    Task SaveFilm(FilmDTO filmDTO);
    Task SavePurchase(PurchaseDTO purchaseDTO);
    Task ChangeQuantityInStock(FilmDTO filmDTO);
    Task DeleteFilm(int id);
  }
}
