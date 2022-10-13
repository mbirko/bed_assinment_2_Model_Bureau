using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using model_handin.Data;
using model_handin.DTO;
using model_handin.Interfaces;
using model_handin.Models;

namespace model_handin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ModelDb _context;
        private IJobService _jobService;

        public JobsController(ModelDb context, IJobService jobService)
        {
            _context = context;
            _jobService = jobService;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTONoModels>> GetJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }
            var expenses = _context.Expenses.Where(x => x.JobId == id).ToList();

            job.Expenses = expenses;

            var jobdotnomodel = job.Adapt<JobDTONoModels>();
            return jobdotnomodel;
        }

        // PUT: api/Jobs/appendModel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("appendModel/{jobId}/{modelId}")]
        public async Task<IActionResult> PutAppendModelJob(long jobId, long modelId)
        {
            var model = _context.Models.FirstOrDefault(x => x.ModelId == modelId);
            if (model == null)
            {
                return NotFound();
            }

            var job = _context.Jobs.FirstOrDefault(x => x.JobId == jobId);
            if (job == null)
            {
                return NotFound();
            }

            job.Models!.Add(model);
            
            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        // PUT: api/Jobs/removeModel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("removeModel/{jobId}/{modelId}")]
        public async Task<IActionResult> PutRemoveModelJob(long jobId, long modelId)
        {
            var job = _context.Jobs.FirstOrDefault(x => x.JobId == jobId);
            if (job == null)
            {
                return NotFound();
            }
            // if the model is within the jobs list, 
            var model = _context.Models.FirstOrDefault(x => x.ModelId == modelId);
            if (model == null)
            {
                return NotFound();
            }
            
            job.Models!.Remove(model);
            
            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        
        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(long id, jobDtoPutUpdate jobDTO)
        {
            var job = _context.Jobs.FirstOrDefault(x => x.JobId == id);
            if (job == null)
            {
                return NotFound();
            }
            
            job = _jobService.UpdateJob(job, jobDTO);
            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(JobDTO jobDto)
        {
            var job = _jobService.ConvertToJob(jobDto);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.JobId }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
