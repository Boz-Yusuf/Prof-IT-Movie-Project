﻿using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class CategoryRepository : EntityRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> AddAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> isExist(string CategoryName)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryName == CategoryName);
        }
    }
}
