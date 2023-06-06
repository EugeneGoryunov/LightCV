using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using LightCV.BL.Exception;
using LightCV.DAL;
using LightCV.DAL.Models;

namespace LightCV.BL.Auth;

public class AuthBL : IAuthBL
{
    private readonly IAuthDal authDal;
    private readonly IEncrypt encrypt;
    private readonly IHttpContextAccessor httpContextAccessor;
    
    public AuthBL(
        IAuthDal authDal,
        IEncrypt encrypt,
        IHttpContextAccessor httpContextAccessor)
    {
        this.authDal = authDal;
        this.encrypt = encrypt;
        this.httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<int> CreatUser(UserModel user)
    {
        user.Salt = Guid.NewGuid().ToString();
        user.Password = encrypt.HashPassword(user.Password, user.Salt);
        int id = await authDal.CreatUser(user);
        Login(id);
        return id;
    }

    public void Login(int id)
    {
       httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSOIN_PARAM_NAME, id);
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await authDal.GetUserByEmail(email);

        if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
        {
            Login(user.UserId ?? 0);
            return user.UserId ?? 0;
        }

        throw new AuthorizationException();
    }

    public async Task<ValidationResult> ValidateEmail(string email)
    {
        var user = await authDal.GetUserByEmail(email);
        
        if (user.UserId != null)
        {
            return new ValidationResult("Email уже существует");
        }

        return null;
    }
}