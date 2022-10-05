﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using model_handin.Data;
using model_handin.DTO;
using model_handin.Interfaces;
using model_handin.Models;
using model_handin.Services;

namespace model_handin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ModelDb _context;
        private IModelService _modelService; 

        public ModelsController(ModelDb context , IModelService modelService)
        {
            _context = context;
            _modelService = modelService;
        }

        // GET: api/Models
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelDTO>>> GetModels()
        {
            var models =  await _context.Models.ToListAsync();
            return _modelService.ConvertToDtO(models);
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(long id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(long id, ModelDTO modelDto)
        {
            if (id != modelDto.ModelId)
            {
                return BadRequest();
            }

            var model = _modelService.ConvertToModel(modelDto);
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel(ModelDTO model)
        {
        
            var modelToAdd = _modelService.ConvertToModel(model);
            _context.Models.Add(modelToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.ModelId }, model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.ModelId == id);
        }
    }
}
