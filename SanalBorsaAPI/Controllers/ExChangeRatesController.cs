using Microsoft.AspNetCore.Mvc;
using SanalBorsaAPI.Core.Entities;
using SanalBorsaAPI.Core.PageData;
using SanalBorsaAPI.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanalBorsaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExChangeRatesController : ControllerBase
    {
        private readonly IRepository<ExChangeRates> _service;
        public ExChangeRatesController(IRepository<ExChangeRates> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 0)
        {
            var itemCount = _service.Count();
            var perPageItem = 20;
            var currentPage = page;
            var golds = await _service.GetPerPageItem(currentPage, perPageItem);
            var pagedResult = new ReturnPagedData<dynamic>();
            pagedResult.ItemCount = itemCount;
            pagedResult.PageCount = (int)Math.Ceiling(itemCount / (decimal)perPageItem);
            pagedResult.CurrentPage = currentPage;
            pagedResult.Items = new List<dynamic>();
            foreach (var item in golds)
            {
                pagedResult.Items.Add(item);
            }
            return Ok(pagedResult);
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
