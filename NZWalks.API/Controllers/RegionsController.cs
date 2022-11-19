using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionsRepository regionsRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
        {
            this.regionsRepository = regionsRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionsRepository.GetAllRegionsAsync();

            //// return DTO of Regions

            //var regionsDTO = new List<RegionDTO>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Latitude = region.Latitude,
            //        Longtitude = region.Longtitude,
            //        Population = region.Population

            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionsRepository.GetRegionAsync(id); 

            if(region == null)
            {
                return NotFound();
            }
            var response = mapper.Map<RegionDTO>(region);

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync([FromBody]AddRegionRequest addRegionRequest)

        {
            // Convert request to Domain model
            var request = new Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Latitude = addRegionRequest.Latitude,
                Longtitude = addRegionRequest.Longtitude,
                Population = addRegionRequest.Population,
                Area = addRegionRequest.Area
            };
            // Pass details to the Repository
            var response = await regionsRepository.AddRegionAsync(request);
            // Convert data back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = response.Id,
                Code = response.Code,
                Name = response.Name,
                Latitude = response.Latitude,
                Longtitude = response.Longtitude,
                Population = response.Population,
                Area = response.Area
            };
            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDTO.Id}, regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from the database
            var request = await regionsRepository.DeleteRegionAsync(id);
            // If result is null, send NotFound
            if(request == null)
            {
                return NotFound();
            }
            // Convert response back to DTO
            var response = new RegionDTO()
            {
                Id = request.Id,
                Code = request.Code,
                Name = request.Name,
                Latitude = request.Latitude,
                Longtitude = request.Longtitude,
                Population = request.Population,
                Area = request.Area
            };
            // Return Ok response 
            return Ok(response);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to Domain model
            var request = new Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Latitude = updateRegionRequest.Latitude,
                Longtitude = updateRegionRequest.Longtitude,
                Population = updateRegionRequest.Population,
                Area = updateRegionRequest.Area

            };
            // Update Region using Repository
            var response = await regionsRepository.UpdateRegionAsync(id, request);
            // If null = NotFound()
            if(response == null)
            {
                return NotFound();
            }
            // Convert Domain back to DTO
            var responseDTO = new RegionDTO()
            {
                Id = response.Id,
                Code = response.Code,
                Name = response.Name,
                Latitude = response.Latitude,
                Longtitude = response.Longtitude,
                Population = response.Population,
                Area = response.Area
            };
            // Return Ok response
            return Ok(responseDTO);
        }
    }
}
