﻿using FilmProject.Domain.Entities;

namespace FilmProject.Presentation.Models
{
    public class AdminCommentViewModel:BaseModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsConfirm { get; set; }
        public string MovieName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
    }
}
