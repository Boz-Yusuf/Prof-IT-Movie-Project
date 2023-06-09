﻿using AutoMapper;
using AutoMapper.Execution;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using FilmProject.Infrastructure.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _repository;
        private IMapper _mapper;
        public MovieService(IMovieRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }


        public int Add(MovieDto movieDto)
        {
            // Veritabanında bu isimle bir film var mı kontrolü yapıldı.
            Movie movie = _mapper.Map<MovieDto, Movie>(movieDto);

            if (_repository.isExist(movie.MovieName))
            {
                throw new InvalidOperationException("This movie is already exists.");
            }
            else
            {          
                var SavedMovie = _repository.Add(movie); 
                return SavedMovie.Id;
            }
        }
        public void Update(MovieDto movieDto)
        {
            Movie movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _repository.Update(movie);
        }

        public async Task<IEnumerable<MovieDto>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null)
        {
            var movies = await _repository.GetListAsync(filter);
            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;

        }

        public async Task<IEnumerable<string>> GetAllLanguagesAsync()
        {
            return await _repository.GetAllLanguagesAsync();
        }

        public async Task<MovieDto?> GetAsync(Expression<Func<Movie, bool>> filter)
        {
            var movie = await _repository.GetAsync(filter);
            MovieDto movieDto = _mapper.Map<Movie,MovieDto>(movie);

            return movieDto;
       
        }

        public async Task<IEnumerable<MovieDto>> GetListWithCategoryAsync(string category)//
        {
            var movies = await _repository.GetListWithCategoryAsync(category);

            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;
        }

        public async Task<IEnumerable<MovieDto>> GetMovieByLanguageAsync(string language)
        {
            var movies = await _repository.GetListAsync(m => m.MovieLanguage == language);

            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;

           
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _repository.GetMovieCountAsync();
        }
     

        public async Task<MovieDto?> GetWithCategoryAsync(Expression<Func<Movie, bool>> filter)
        {
           Movie model = await _repository.GetWithCategoryAsync(filter);
           MovieDto movie = _mapper.Map<Movie, MovieDto>(model);
           return movie;
        }

    }
}
