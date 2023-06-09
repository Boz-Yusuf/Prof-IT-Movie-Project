﻿using FilmProject.Domain.Entities;
using FilmProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Contracts.Movie
{
    public class MovieDto:BaseDto
    {
        public string MovieName { get; set; }
        public string MovieSummary { get; set; }
        public string DirectorName { get; set; }
        public MovieRatings RatingAge { get; set; }
        public DateTime PublishYear { get; set; }
        public string PhotoPath { get; set; }
        public bool IsHighLighted { get; set; }
        public string ImdbUrl { get; set; }
        public string MovieLanguage { get; set; }
        public bool isDeleted { get; set; }

        public ICollection<MovieLikeDto> MovieLikes { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<CategoryDto> MovieCategories { get; set; }
        public ICollection<FavoriteDto> WhoFavorited { get; set; }
    }
}
