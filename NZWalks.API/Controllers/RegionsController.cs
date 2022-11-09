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
        public async Task<IActionResult> GetAllRegions()
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
    }
}
