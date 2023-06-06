using LightCV.DAL.Models;

namespace LightCV.DAL;

public interface IAuthDal
{
    Task<UserModel> GetUserByEmail(string email);
    Task<UserModel> GetUserById(string id);
    Task<int> CreatUser(UserModel model);
}