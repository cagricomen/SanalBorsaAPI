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
    public class ExChangeRatesController : ControllerBase
    {
        private readonly IService<CryptoCurrency> _service;
        public ExChangeRatesController(IService<CryptoCurrency> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exChangeRates = await _service.GetAllAsync();
            return Ok(exChangeRates);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exChangeRate = await _service.GetByIdAsync(id);
            if (exChangeRate != null)
            {
                return Ok(exChangeRate);
            }
            else
            {
                return BadRequest("item not found");
            }
        }
    }
}
