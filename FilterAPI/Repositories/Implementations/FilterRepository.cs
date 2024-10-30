using FilterAPI.Data;
using FilterAPI.Models.Domain;
using FilterAPI.Models.Requests;
using FilterAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FilterAPI.Repositories.Implementations
{
    public class FilterRepository : IFilterRepository
    {
        private readonly FilterDbContext _dbContext;

        public FilterRepository(FilterDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Product>> GetFilteredProductDetails(FilterRequest filterRequest) { }
    }
}
