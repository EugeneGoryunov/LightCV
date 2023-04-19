namespace CurriculumVitae.BL.Auth;

public interface IAuthBL
{
    Task<int> CreatUser(CurriculumVitae.DAL.Models.UserModel user);
}