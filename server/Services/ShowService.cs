using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Shows;

namespace WebApi.Services
{
    public class ShowService : IShowService
    {
        private DataContext _context;
        private readonly IMapper _mapper;

        public ShowService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ShowDto> GetAll()
        {
            var shows = _context.Shows.Include(x => x.ShowCategories);
            var dtos = _mapper.Map<IEnumerable<ShowDto>>(shows);
            return dtos;
        }

        public ShowDto GetById(int id)
        {
            var show = getShow(id);
            var dto = _mapper.Map<ShowDto>(show);
            return dto;
        }

        public void Create(CreateShowRequestDto model)
        {
            Create(model, true);
        }
        
        private void Create(CreateShowRequestDto model, bool save)
        {
            var show = _mapper.Map<Show>(model);
            
            // Create categories as needed and add them to show
            foreach (var category in model.Categories)
            {
                var cat = _context.ShowCategories.FirstOrDefault(x => x.Name == category);
                
                if (cat == null)
                {
                    var newCat = new ShowCategory();
                    newCat.Name = category;
                    _context.ShowCategories.Add(newCat);
                    show.ShowCategories.Add(newCat);
                }
                else
                {
                    show.ShowCategories.Add(cat);
                }
            }
            
            _context.Shows.Add(show);
            
            if (save)
            {
                _context.SaveChanges();    
            }
        }
        
        

        public void CreateMany(List<ShowCsvRowDto> csvRows)
        {
            var categories = new HashSet<string>();
            
            foreach (var csvRow in csvRows)
            {
                foreach (var category in csvRow.ShowCategorysCsv.Split(","))
                {
                    categories.Add(category.Trim());
                }
            }

            foreach (var category in categories)
            {
                var cat = _context.ShowCategories.FirstOrDefault(x => x.Name == category);

                if (cat == null)
                {
                    var newCat = new ShowCategory();
                    newCat.Name = category;
                    _context.ShowCategories.Add(newCat);
                }
            }
            
            _context.SaveChanges();
            
            foreach (var csvRow in csvRows)
            {
                var show = _mapper.Map<CreateShowRequestDto>(csvRow);

                foreach (var category in csvRow.ShowCategorysCsv.Split(","))
                {
                    show.Categories.Add(category.Trim());
                }
                
                Create(show, false);
            }
            
            _context.SaveChanges();
        }

        public void Update(int id, UpdateShowRequestDto model)
        {
            var show = getShow(id);
            _mapper.Map(model, show);
            
            // Clear existing categories
            show.ShowCategories.Clear();
            _context.SaveChanges();
            
            // Create categories as needed and add them to show
            foreach (var category in model.Categories)
            {
                var cat = _context.ShowCategories.FirstOrDefault(x => x.Name == category);
                
                if (cat == null)
                {
                    var newCat = new ShowCategory();
                    newCat.Name = category;
                    _context.ShowCategories.Add(newCat);
                    show.ShowCategories.Add(newCat);
                }
                else
                {
                    show.ShowCategories.Add(cat);
                }
            }
            
            _context.Shows.Update(show);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var show = getShow(id);
            _context.Shows.Remove(show);
            _context.SaveChanges();
        }

        // helper methods

        private Show getShow(int id)
        {
            var show = _context.Shows.Include(x => x.ShowCategories).Single(x => x.Id == id);
            if (show == null) throw new KeyNotFoundException("User not found");
            return show;
        }
    }
}
