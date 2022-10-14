using model_handin.Models;
using model_handin.DTO;
using model_handin.Interfaces;
using Mapster;

namespace model_handin.Services
{
    public class ModelService  : IModelService 
    {

        public ModelService()
        {
            TypeAdapterConfig<ModelDTO, Model>
                .NewConfig()
                .IgnoreNullValues(true);
        }
        public List<ModelDTO> ConvertToDtO(List<Model> models)
        {
            var modelsDto = models.Adapt<List<ModelDTO>>();
            return modelsDto;
        }

        public Model ConvertToModel(ModelDTO model)
        {
            var toModel = model.Adapt<Model>();
            return toModel;
        }

        public Model UpdateModel(Model model, ModelPutDto update)
        {
            update.Adapt(model);
            return model;
        }
        
    }
}