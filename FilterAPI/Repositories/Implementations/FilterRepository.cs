using AutoMapper;
using Azure.Core;
using FilterAPI.Data;
using FilterAPI.Models.Domain;
using FilterAPI.Models.Requests;
using FilterAPI.Models.Responses;
using FilterAPI.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace FilterAPI.Repositories.Implementations
{
    public class FilterRepository : IFilterRepository
    {
        private readonly FilterDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public FilterRepository(FilterDbContext dbContext, ILogger logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get Filtered Product Detail
        /// </summary>
        /// <param name="filterRequest"></param>
        /// <returns>Filtered/Sorted Records from Product Table</returns>
        public async Task<List<PaginatedFilteredResponse>> GetFilteredProductDetails(
            FilterRequest filterRequest
        )
        {
            IQueryable<Product> data;
            List<PaginatedFilteredResponse> response = new();
            var sortRequest = filterRequest.SortRequest;
            var paginationRequest = filterRequest.PaginationRequest;
            List<Product> filteredData = new();
            if (filterRequest != null)
            {
                _logger.LogInformation(
                    "Entered GetFilteredProductDetails Method in FilterRepository"
                );
                #region Insert into Search History Table
                var searchHistory = new SearchHistory
                {
                    Id = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    Query = JsonConvert.SerializeObject(filterRequest)
                };
                await _dbContext.SearchHistory.AddAsync(searchHistory);
                await _dbContext.SaveChangesAsync();
                #endregion
                #region Filter
                if (CheckIfFilterRequestNotEmpty(filterRequest))
                {
                    data =
                        (IQueryable<Product>)
                            _dbContext.Products.Where(
                                x =>
                                    (
                                        (
                                            filterRequest.Name == null
                                            || !filterRequest.Name.Any()
                                            || filterRequest.Name.Contains(x.Name)
                                        )
                                        && (
                                            filterRequest.Type == null
                                            || !filterRequest.Type.Any()
                                            || filterRequest.Type.Contains(x.Type)
                                        )
                                        && (
                                            filterRequest.StocksPurchased == null
                                            || !filterRequest.StocksPurchased.Any()
                                            || filterRequest.StocksPurchased.Contains(
                                                x.StockQuantity
                                            )
                                        )
                                        && (
                                            (
                                                filterRequest.MfgStartDate == null
                                                && filterRequest.MfgEndDate == null
                                            )
                                            || (
                                                x.MfgDate <= filterRequest.MfgEndDate
                                                && x.MfgDate >= filterRequest.MfgStartDate
                                            )
                                        )
                                    )
                            );
                }
                else
                {
                    data = _dbContext.Products.AsQueryable();
                }
                filteredData = await data.ToListAsync();
                #endregion
                #region Sorting
                if (sortRequest != null)
                {
                    if (CheckIfSortRequestNotEmpty(sortRequest))
                    {
                        if (sortRequest.IsDateAscending != null)
                        {
                            filteredData =
                                sortRequest.IsDateAscending == true
                                    ? filteredData.OrderBy(x => x.MfgDate).ToList()
                                    : filteredData.OrderByDescending(x => x.MfgDate).ToList();
                        }
                        if (sortRequest.IsPopularAscending != null)
                        {
                            filteredData =
                                sortRequest.IsPopularAscending == true
                                    ? filteredData.OrderBy(x => x.StockQuantity).ToList()
                                    : filteredData.OrderByDescending(x => x.StockQuantity).ToList();
                        }
                        if (sortRequest.IsRelevant != null)
                        {
                            filteredData = filteredData
                                .OrderByDescending(
                                    x =>
                                        (x.StockQuantity / 5) * 2
                                        + (x.MfgDate.Month == DateTime.Now.Month ? 10 : 0)
                                )
                                .ToList();
                        }
                    }
                }
                #endregion
                #region Pagination
                if (paginationRequest != null)
                {
                    var skipResults = (paginationRequest.Page - 1) * paginationRequest.PageSize;
                    filteredData = filteredData
                        .Skip(skipResults)
                        .Take(paginationRequest.PageSize)
                        .ToList();
                }
                #endregion
            }
            response = _mapper.Map<List<PaginatedFilteredResponse>>(filteredData);
            _logger.LogInformation("Response for Request : {0} is {1}", filterRequest, response);
            return response;
        }

        /// <summary>
        /// Check If the Filter Model is Empty or Not
        /// </summary>
        /// <param name="filterRequest"></param>
        /// <returns>Returns true or false</returns>
        public bool CheckIfFilterRequestNotEmpty(FilterRequest filterRequest)
        {
            if (
                filterRequest.Name != null
                || filterRequest.Type != null
                || filterRequest.StocksPurchased != null
                || filterRequest.MfgEndDate != null
                || filterRequest.MfgStartDate != null
            )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check If the Sort Model is Empty or Not
        /// </summary>
        /// <param name="sortRequest"></param>
        /// <returns>Returns true or false</returns>
        public bool CheckIfSortRequestNotEmpty(SortRequest sortRequest)
        {
            if (
                sortRequest.IsRelevant != null
                || sortRequest.IsPopularAscending != null
                || sortRequest.IsDateAscending != null
            )
            {
                return true;
            }

            return false;
        }
    }
}
