namespace WebApi.Services;

using WebApi.Entities;
using WebApi.Models.Shows;

public interface IShowCategoryService
{
    IEnumerable<string> GetAll();
    void Create(string category);
    void Delete(string category);
}

