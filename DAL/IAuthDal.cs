using CurriculumVitae.DAL.Models;

namespace CurriculumVitae.DAL;

public interface IAuthDal
{
    Task<UserModel> GetUserByEmail(string email);
    Task<UserModel> GetUserById(string id);
    Task<int> CreatUser(UserModel model);
}