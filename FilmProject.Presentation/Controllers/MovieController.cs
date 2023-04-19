﻿using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetCount")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMovieCountAsync() // film sayısı
        {
            var count = await _movieService.GetMovieCountAsync();
            var result = new { Count = count };
            return Json(result);
        }

        [HttpGet]
        [Route("Movie/{id}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMovieAsync(int id) // tek bir film , id ile . Detay sayfası için 
        {
            // burda include yapmadan çekiyoruz categorisiz, Yorumsuz ,favorileri alanlar hariç listeleniyor 
                // daha sonra istersek bu id le o listelerde filtreleme yapabilriz.
            try
            {
                var movies = await _movieService.GetAsync(x => x.Id == id);
                return Json(movies);
            }
            catch
            {
                return Ok(new
                {
                    mesaj = "Geçersiz Id Değeri "
                });
            }
        }

        [HttpGet]
        [Route("Movies")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesAsync() // tum filmler 
        {


            var movies = await _movieService.GetAllAsync();

            return Json(movies);
        }
        [HttpGet]
        [Route("MoviesWithCategory")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetMoviesWithCategoryAsync() // tum filmler , categoriler ile birlikte.
        {
            var movies = await _movieService.GetListWithCategoryAsync();
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(movies, settings);


            return Ok(json);
        }

        [HttpGet]
        [Route("GetLastMovies/{number}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetLastMoviesAsync(int number) // son yuklenen 
        {
            var movies = await _movieService.GetLastMoviesAsync(number);

            return Json(movies);
        }



        [HttpPost]
        [Route("Create")]
        public IActionResult CreateMovie([FromBody] Movie model) // Film Ekleme fonksiyonu 
        {
            model.Created = DateTime.UtcNow;
            _movieService.Add(model);
            return Ok(model);
        }
    }
}
