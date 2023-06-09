﻿using AutoMapper;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Domain.Entities;
using FilmProject.Presentation.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmProject.Presentation.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMovieCategoryMapService _movieCategoryMapService;
        private readonly IValidator<CategoryViewModel> _validator;
        private IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMovieCategoryMapService movieCategoryMapService, IMapper mapper, IValidator<CategoryViewModel> validator)
        {

            _mapper = mapper;
            _categoryService = categoryService;
            _validator = validator;
            _movieCategoryMapService = movieCategoryMapService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateCategory")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel categoryViewModel)
        {

            if (!ModelState.IsValid)
            {
                Exception ex = new Exception(
                    message: "Lutfen Kategori Adı Giriniz"
                    );
                return BadRequest(ex.Message);
            }

            CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
            try
            {
                await _categoryService.AddCategory(category);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                //Loglama yapılabilir.
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ListAll")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetCategoriesAsync() // kategoriler listeleniyor sayısı
        {
            var Liste = await _categoryService.GetAllAsync(x=>x.isDeleted==false);
          
            return Json(Liste);
        }
        [HttpGet]
        [Route("GetCount")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetCategoryCount() // kategoriler listeleniyor sayısı
        {
            var count = await _categoryService.CountAsync();

            return Json(count);
        }
        [HttpGet]
        [Route("Categories")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetCategoriesReturnPartial() // tum KategorileriDondurur
        {
            IEnumerable<CategoryDto> categories = await _categoryService.GetAllAsync(x=>x.isDeleted==false);
          
            List<CategoryDto> onlyconntectedCategories = new List<CategoryDto>(); // mapper da en az 1 filme sahip kategoriler
            foreach (var cate in categories)
            {
                var result = await _movieCategoryMapService.AnyMoviesInThisCategory(x => x.CategoryId == cate.Id);
                if (result == true)
                {
                    onlyconntectedCategories.Add(cate);
                }

            }
            IEnumerable<CategoryViewModel> categoryViewModel = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<CategoryViewModel>>(onlyconntectedCategories);
            return PartialView(@"~/Views/Home/_RenderCategories.cshtml", categoryViewModel);
        }
        [HttpPost]
        [Route("UpdateCategory")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategoryy([FromBody] CategoryViewModel categoryViewModel)
        {
            CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Ok();
            }
            catch (Exception ex)
            {
                //Loglama yapılabilir.
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("AllCategories")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                IEnumerable<CategoryDto> categories = await _categoryService.GetAllAsync(x=>x.isDeleted==false);
                IEnumerable<CategoryViewModel> categoryViewModel = _mapper.Map<IEnumerable<CategoryDto>, IEnumerable<CategoryViewModel>>(categories);


                return Ok(categoryViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

            [HttpPost]
            [Route("Delete")]
            //[Authorize(Roles = "Admin")]
            public async Task<IActionResult> DeleteCategory(int id)
            {
                try
                {
                    await _categoryService.DeleteCategoryAsync(id);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    //Loglama yapılabilir.
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            [Route("Update")]
            //[Authorize(Roles = "Admin")]
            public async Task<IActionResult> UpdateCategory([FromBody] CategoryViewModel categoryViewModel)
            {
                CategoryDto category = _mapper.Map<CategoryViewModel, CategoryDto>(categoryViewModel);
                try
                {
                    await _categoryService.UpdateCategoryAsync(category);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    //Loglama yapılabilir.
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }

            }

        [HttpGet]
        [Route("MostPopularCategory")]
        public async Task<IActionResult> GetMostPopularCategoryMetric()
        {
            try
            {
                var result = await _categoryService.GetMostPopularCategoryAsync();
                return Json(new { Name = result.CategoryName, Count = result.MovieCount});
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
    }
} 
