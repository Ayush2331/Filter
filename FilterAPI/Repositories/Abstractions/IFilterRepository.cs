using FilterAPI.Models.Domain;
using FilterAPI.Models.Requests;
using FilterAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FilterAPI.Repositories.Abstractions
{
    public interface IFilterRepository
    {
        Task<List<PaginatedFilteredResponse>> GetFilteredProductDetails(
            FilterRequest filterRequest
        );
    }
}
