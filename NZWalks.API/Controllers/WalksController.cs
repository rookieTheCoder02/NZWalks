using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fetch data from database - domain model
            var request = await walksRepository.GetAllWalksAsync();
            // Convert domain model to DTO model
            var response = mapper.Map<List<WalkDTO>>(request);
            // Return response
            return Ok(response);

        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get request domain object from database
            var request = await walksRepository.GetWalkAsync(id);
            // Convert request object to DTO
            var response = mapper.Map<WalkDTO>(request);
            // Return response
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody]AddWalkRequest addWalkRequest)
        {
            // Convert request to domain model
            var request = new Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            // Pass details to the response
            var response = await walksRepository.AddWalkAsync(request);
            // Convert response to DTO model
            var responseDTO = new WalkDTO()
            {
                Id = response.Id,
                Name = response.Name,
                Length = response.Length,
                RegionId = response.RegionId,
                WalkDifficultyId = response.WalkDifficultyId
            };
            // Return DTO
            return CreatedAtAction(nameof(GetWalkAsync), new {id = responseDTO.Id}, responseDTO);
        }
    }
}
