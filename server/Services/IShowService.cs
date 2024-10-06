using WebApi.Models;

namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Models.Shows;

public interface IShowService
{
    IEnumerable<ShowDto> GetAll();
    PaginatedList<ShowDto> GetAll(int pageIndex, int pageSize);
    ShowDto GetById(int id);
    void Create(CreateShowRequestDto model);
    void CreateMany(List<ShowCsvRowDto> csvRows);
    void Update(int id, UpdateShowRequestDto model);
    void Delete(int id);

    List<string> GetAllRatings();
    List<string> GetAllCountries();
}

