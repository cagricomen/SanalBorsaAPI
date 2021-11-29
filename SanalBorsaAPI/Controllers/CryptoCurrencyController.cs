using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Core.Services;
using SanalBorsaAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly IService<CryptoCurrency> _service;
        public CryptoCurrencyController(IService<CryptoCurrency> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await _service.GetAllAsync();
            return Ok(currencies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currency = await _service.GetByIdAsync(id);
            if (currency != null)
            {
                return Ok(currency);
            }
            else
            {
                return BadRequest("item not found");
            }
        }
    }
}
