using DapperProject.Dtos.CategoryDtos;
using DapperProject.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperProject.Repository
{
    public interface IProductRepository
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
    }
}
