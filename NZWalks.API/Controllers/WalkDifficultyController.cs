using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Runtime.Serialization.Formatters;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var request = await walkDifficultyRepository.GetAllWalkDifficultiesAsync();
            if(request == null) return NotFound();
            var response = mapper.Map<List<WalkDifficultyDTO>>(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var request = await walkDifficultyRepository.GetWalkDifficultyAsync(id);
            if (request == null) return NotFound();
            var response = mapper.Map<WalkDifficultyDTO>(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody]AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // Convert add request model into request Domain model
            var request = new WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            // If request is null = return NotFound()
            if (request == null) return NotFound();
            // If request is not null = return response
            var response = await walkDifficultyRepository.AddWalkDifficultyAsync(request);
            // Convert response into DTO response model
            var responseDTO = mapper.Map<WalkDifficultyDTO>(response);
            // Return responseDTO
            return Ok(responseDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            // Create request to delete model object from db
            var request = await walkDifficultyRepository.DeleteWalkDifficultyAsync(id);
            // If value of object == null, return NotFound()
            if (request == null) return NotFound();
            // If value of object != null, create responseDTO object
            var responseDTO = mapper.Map<WalkDifficultyDTO>(request);
            // return responseDTO
            return Ok(responseDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute]Guid id, 
            [FromBody]UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Convert add request model into request Domain model
            var request = new WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };
            // If value of object != null, create responseDTO object
            var response = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, request);
            // If value of object == null, return NotFound()
            if (response == null)
            {
                return NotFound();
            }
            // Convert response into DTO response model
            var responseDTO = mapper.Map<WalkDifficultyDTO>(response);
            // return responseDTO
            return Ok(responseDTO);
        }
    }
}
