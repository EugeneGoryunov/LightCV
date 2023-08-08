using LightCV.DAL.Models;

namespace LightCV.DAL;

public interface IAuthDal
{
    Task<UserModel> GetUserByEmail(string email);
    Task<UserModel> GetUserById(int id);
    Task<int> CreatUser(UserModel model);
}