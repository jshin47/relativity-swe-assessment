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
            // This is dangerous because the shows to create may not fit in memory, but for now let's ignore it.

            var showsToCreate = new List<CreateShowRequestDto>();
            
            // Build a set of all categories that need to be created.
            
            var categories = new HashSet<string>();
            
            foreach (var csvRow in csvRows)
            {
                var show = _mapper.Map<CreateShowRequestDto>(csvRow);
                
                foreach (var category in csvRow.ShowCategorysCsv.Split(","))
                {
                    var cat = category.Trim();
                    categories.Add(cat);
                    show.Categories.Add(cat);
                }
                
                showsToCreate.Add(show);
            }
            
            // Create all categories before attempting to create shows.

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
            
            // Create the shows
            
            foreach (var show in showsToCreate)
            {
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

        public List<string> GetAllRatings()
        {
            return _context.Shows.Select(x => x.Rating).Distinct().OrderBy(x => x).ToList();
        }

        public List<string> GetAllCountries()
        {
            return _context.Shows.Select(x => x.Country).Distinct().OrderBy(x => x).ToList();
        }

        // helper methods

        private Show getShow(int id)
        {
            var show = _context.Shows.Include(x => x.ShowCategories).Single(x => x.Id == id);
            if (show == null) throw new KeyNotFoundException("Show not found");
            return show;
        }
    }
}
