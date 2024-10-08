﻿using Microsoft.AspNetCore.OutputCaching;

namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Shows;
using WebApi.Services;

[ApiController]
[Route("api/v1/[controller]")]
public class ShowsController : ControllerBase
{
    private IShowService _ShowService;
    private IMapper _mapper;

    public ShowsController(IShowService ShowService, IMapper mapper)
    {
        _ShowService = ShowService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll(int pageIndex = 1, int pageSize = 10)
    {
        var Shows = _ShowService.GetAll(pageIndex, pageSize);
        return Ok(Shows);
    }

    [HttpGet("countries")]
    public IActionResult GetAllCountries()
    {
        var countries = _ShowService.GetAllCountries();
        return Ok(countries);
    }
    
    [HttpGet("ratings")]
    public IActionResult GetAllRatings()
    {
        var ratings = _ShowService.GetAllRatings();
        return Ok(ratings);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var Show = _ShowService.GetById(id);
        return Ok(Show);
    }

    [HttpPost]
    public IActionResult Create(CreateShowRequestDto model)
    {
        _ShowService.Create(model);
        return Ok(new { message = "Show created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateShowRequestDto model)
    {
        _ShowService.Update(id, model);
        return Ok(new { message = "Show updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _ShowService.Delete(id);
        return Ok(new { message = "Show deleted" });
    }
}