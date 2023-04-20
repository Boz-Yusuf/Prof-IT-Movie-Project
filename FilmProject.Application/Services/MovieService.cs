﻿using AutoMapper;
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

        public void Add(Movie movie)
        {
            // Veritabanında bu isimle bir kategori var mı kontrolü yapıldı.
            if (_repository.isExist(movie.MovieName))
            {
                throw new InvalidOperationException("This movie is already exists.");
            }
            else
            {
                 _repository.Add(movie);
            }
           
        }
        public void Update(Movie movie)
        {
            _repository.Update(movie);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null)
        {
            return await _repository.GetListAsync();
        }
     
        public async Task<IEnumerable<string>> GetAllLanguagesAsync()
        {
            return await _repository.GetAllLanguagesAsync();
        }

        public async Task<Movie?> GetAsync(Expression<Func<Movie, bool>> filter)
        {
            return await _repository.GetAsync(filter);
        }

        public async Task<IEnumerable<Movie>> GetLastMoviesAsync(int number)
        {
            return await _repository.GetLastMovieAsync(number);
        }

        public async Task<IEnumerable<MovieDto>> GetListWithCategoryAsync()//
        {
            var movies = await _repository.GetListWithCategoryAsync();

            List<MovieDto> movieDto = _mapper.Map< List<Movie>, List<MovieDto> >(movies);

            return movieDto;
        }

        public async Task<IEnumerable<Movie>> GetMovieByLanguageAsync(string language)
        {
            return await _repository.GetListAsync(m => m.MovieLanguage == language);
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _repository.GetMovieCountAsync();
        }
  
       
    }
}
