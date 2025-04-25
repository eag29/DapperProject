using DapperProject.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperProject.Repository
{
    public interface ICategoryRepository
    {
        Task<List<ResultCategoryDto> > GetAllCategoryAsync();
    }
}
