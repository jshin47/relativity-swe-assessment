namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Shows;
using WebApi.Services;

[ApiController]
[Route("api/v1/[controller]")]
public class ShowCategorysController : ControllerBase
{
    private IShowCategoryService _showCategoryService;

    public ShowCategorysController(IShowCategoryService showCategoryService)
    {
        _showCategoryService = showCategoryService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _showCategoryService.GetAll();
        return Ok(categories);
    }

    [HttpPost]
    public IActionResult Create(string name)
    {
        _showCategoryService.Create(name);
        return Ok(new { message = "ShowCategory created" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string name)
    {
        _showCategoryService.Delete(name);
        return Ok(new { message = "ShowCategory deleted" });
    }
}