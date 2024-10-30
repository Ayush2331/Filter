using FilterAPI.Models.Domain;
using FilterAPI.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FilterAPI.Repositories.Abstractions
{
    public interface IFilterRepository
    {
        Task<List<Product>> GetFilteredProductDetails(FilterRequest filterRequest);
    }
}
