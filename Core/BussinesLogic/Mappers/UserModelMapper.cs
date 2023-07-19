using Recommender.Core.Models;

namespace Recommender.BussinesLogic.Mappers
{
    public static class UserModelMapper
    {
        public static UserModel MapToUserModel(this LogInResponse response)
        {
            return new UserModel
            {
                Id = response.Id,
                FullName = response.FullName,
                Email = response.Email,
                ImageUrl = response.ImageUrl,
                Gender = response.Gender,
                BirthDate = response.BirthDate
            };
        }
    }
}
