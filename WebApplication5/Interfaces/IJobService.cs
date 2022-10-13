using model_handin.DTO;
using model_handin.Models;

namespace model_handin.Interfaces
{
    public interface IJobService
    {
        JobDTO ConvertToDTOJob(Job job);
        Job ConvertToJob(JobDTO jobDTO);
        Job UpdateJob(Job destination, jobDtoPutUpdate source);

        //Model ConvertToModel(ModelDTO model);

        //List<ModelDTO> ConvertToDtO(List<Model> model);
    }
}