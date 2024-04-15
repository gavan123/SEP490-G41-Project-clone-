using BusinessObject.DTO;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/maps")]
    [ApiController]
    public class MapsController : ODataController
    {
        private readonly IMapRepository _mapRepository;

        public MapsController(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        // GET: api/maps
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllMaps()
        {
            var maps = _mapRepository.GetAllMaps();
            return Ok(maps);
        }

        // GET: api/maps/5
        [HttpGet("{id}")]
        public IActionResult GetMapById(int id)
        {
            if (id <= 0)
                return BadRequest("Map ID must be a positive integer.");

            var map = _mapRepository.GetMapById(id);
            if (map == null)
                return NotFound();

            return Ok(map);
        }

        // POST: api/maps
        [HttpPost]
        public ActionResult<MapAddDTO> AddMap(MapAddDTO map)
        {
            if (map == null)
                return BadRequest("Map cannot be null.");

            _mapRepository.AddMap(map);
            return Ok(map);
        }

        // PUT: api/maps/5
        [HttpPut("{id}")]
        public IActionResult UpdateMapById(int id, MapUpdateDTO map)
        {
            if (id <= 0)
                return BadRequest("Map ID must be a positive integer.");

            if (map == null)
                return BadRequest("Map cannot be null.");

            var tmpMap = _mapRepository.GetMapById(id);
            if (tmpMap == null)
                return NotFound();

            _mapRepository.UpdateMap(map);
            return Ok();
        }

        // DELETE: api/maps/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMapById(int id)
        {
            if (id <= 0)
                return BadRequest("Map ID must be a positive integer.");

            var map = _mapRepository.GetMapById(id);
            if (map == null)
                return NotFound();

            _mapRepository.DeleteMap(id);
            return Ok();
        }
    }
}