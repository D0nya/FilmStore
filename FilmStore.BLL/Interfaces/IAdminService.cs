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

    FilmDTO GetFilm(int id);

    void SaveFilm(FilmDTO filmDTO);
    void SavePurchase(PurchaseDTO purchaseDTO);
    void ChangeQuantityInStock(FilmDTO filmDTO);
    void DeleteFilm(int id);
  }
}
