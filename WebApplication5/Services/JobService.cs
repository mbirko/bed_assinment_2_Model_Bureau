using Mapster;
using Microsoft.EntityFrameworkCore;
using model_handin.Data;
using model_handin.DTO;
using model_handin.Interfaces;
using model_handin.Models;
using System.Linq;
namespace model_handin.Services
{
    public class JobService : IJobService
    {

        private readonly ModelDb _dbContext;

        //public List<JobDTO> ConvertToDtO(List<Job> jobs)
        //{
        //    var modelsDto = models.Adapt<List<ModelDTO>>();
        //    return modelsDto;
        //}
        //private DbContext _context;
        public JobService(ModelDb _context)
        {
            _dbContext = _context;
            //_context = _context;
        }
        public Job ConvertToJob(JobDTO jobDTO)
        {
            var job = jobDTO.Adapt<Job>();
            return job;
        }

        //public Job CreateJob(Job job)
        //{
        //    var models = _context.Model.Where(x=>x.ModelId == job.model)
        //}
    }
}