using model_handin.Models;
using model_handin.DTO;

namespace model_handin.Services
{
    public class ModelService
    {
        public Model ConvertModelDtoToModel(ModelDTO modelDto)
        {
            return new Model
            {
                FirstName = modelDto.FirstName,
                LastName = modelDto.LastName,
                AddresLine1 = modelDto.AddresLine1,
                AddresLine2 = modelDto.AddresLine2,
                HairColor = modelDto.HairColor,
                Email = modelDto.Email,
                PhoneNo = modelDto.PhoneNo,
                Zip = modelDto.Zip,
                Height = modelDto.Height,
                City = modelDto.City,
                BirthDay = modelDto.BirthDay,
                ShoeSize = modelDto.ShoeSize,
                Comments = modelDto.Comments,
            };
        }
    }
}