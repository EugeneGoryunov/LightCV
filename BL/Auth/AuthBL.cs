using CurriculumVitae.DAL.Models;
using CurriculumVitae.DAL;

namespace CurriculumVitae.BL.Auth;

public class AuthBL : IAuthBL
{
    private readonly IAuthDal authDal;
    
    public AuthBL(IAuthDal authDal)
    {
        this.authDal = authDal;
    }
    
    public async Task<int> CreatUser(UserModel user)
    {
        return await authDal.CreatUser(user);
    }
}