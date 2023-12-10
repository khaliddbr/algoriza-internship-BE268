using AutoMapper;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VezeetaProject.Controllers
{
    
    public class SettingController : BaseController
    {
        private readonly IGenericRepository<Discount> _discountRepository;
        private readonly IMapper _mapper;
        public SettingController(IGenericRepository<Discount> DiscountRepository, IMapper mapper)
        {
            _discountRepository = DiscountRepository;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Discount>> AddDiscount(Discount discount)
        {
            var addedDiscount = await _discountRepository.AddAsync(discount);
            return CreatedAtAction(nameof("GetDiscountById"), new { id = addedDiscount.Id }, addedDiscount);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDiscount(int id, Discount discount)
        {
            if (id != discount.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingDiscount = await _discountRepository.GetByIdAsync(id);
            if (existingDiscount == null)
            {
                return NotFound();
            }

          

            var updatedDiscount = await _discountRepository.UpdateAsync(discount);
            return Ok(updatedDiscount);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var result = await _discountRepository.RemoveAsync(new Discount { Id = id });
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateDiscount(int id)
        {
            var result = await _discountRepository.DeactivateAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}

