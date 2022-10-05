using model_handin.Models;
using model_handin.DTO;
using model_handin.Interfaces;
using Mapster;

namespace model_handin.Services
{
    public class ModelService  : IModelService 
    {
      
        public List<ModelDTO> ConvertToDtO(List<Model> models)
        {
            var modelsDto = models.Adapt<List<ModelDTO>>();
            return modelsDto;
        }

        public Model ConvertToModel(ModelDTO model)
        {
            var _model = model.Adapt<Model>();
            return _model;
        }

 
        
    }
}