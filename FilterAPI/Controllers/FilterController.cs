using FilterAPI.Models.Domain;
using FilterAPI.Models.Requests;
using FilterAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilterController : ControllerBase
    {
        private readonly IFilterRepository _filterRepository;

        public FilterController(IFilterRepository filterRepository)
        {
            this._filterRepository = filterRepository;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<Product>> GetFilteredProductDetails(
            [FromBody] FilterRequest filterRequest
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filteredProductDetails = await _filterRepository.GetFilteredProductDetails(
                filterRequest
            );
            return Ok(filteredProductDetails);
        }
    }
}
