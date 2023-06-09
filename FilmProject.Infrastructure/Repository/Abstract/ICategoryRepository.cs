using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<bool> isDeletedStatus(string CategoryName);
        bool isExist(string CategoryName);
        bool isExistById(int CategoryId);

        Task<bool> isExistAsync(string CategoryName);
        Task<bool> AddAsync(Category category);

        Task<int> CountAsync();

        Task<Category> GetMostPopularCategoryAsync();


    }
}
