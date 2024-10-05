using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Shows;

namespace WebApi.Services
{
    public class ShowCategoryService : IShowCategoryService
    {
        private DataContext _context;

        public ShowCategoryService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetAll()
        {
            return _context.ShowCategories.Select(c => c.Name).Distinct();
        }

        public void Create(string category)
        {
            var cat = new ShowCategory();
            cat.Name = category;
            
            _context.ShowCategories.Add(cat);
            _context.SaveChanges();
        }

        public void Delete(string category)
        {
            var cat = _context.ShowCategories.Single(c => c.Name == category);
            _context.ShowCategories.Remove(cat);
            _context.SaveChanges();
        }
    }
}
