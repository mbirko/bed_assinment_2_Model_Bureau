using model_handin.DTO;
using model_handin.Models;

namespace model_handin.Interfaces
{
    public interface IModelService
    {
        Model ConvertToModel(ModelDTO model);

        List<ModelDTO> ConvertToDtO(List<Model> model);
    }
}