using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldenController : ControllerBase
    {
        private readonly IService<Golden> _service;
        public GoldenController(IService<Golden> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var goldens = await _service.GetAllAsync();
            return Ok(goldens);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var golden = await _service.GetByIdAsync(id);
            if (golden != null)
            {
                return Ok(golden);
            }
            else
            {
                return BadRequest("item not found");
            }
        }
    }
}
