using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using LightCV.BL.Exception;
using LightCV.BL.General;
using LightCV.DAL;
using LightCV.DAL.Models;

namespace LightCV.BL.Auth;

public class AuthBL : IAuthBL
{
    private readonly IAuthDal authDal;
    private readonly IEncrypt encrypt;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IDbSession dbSession;
    
    public AuthBL(
        IAuthDal authDal,
        IEncrypt encrypt,
        IHttpContextAccessor httpContextAccessor,
        IDbSession dbSession)
    {
        this.authDal = authDal;
        this.encrypt = encrypt;
        this.httpContextAccessor = httpContextAccessor;
        this.dbSession = dbSession;
    }
    
    public async Task<int> CreatUser(UserModel user)
    {
        user.Salt = Guid.NewGuid().ToString();
        user.Password = encrypt.HashPassword(user.Password, user.Salt);
        int id = await authDal.CreatUser(user);
        await Login(id);
        return id;
    }
    
    public async Task ValidateEmail(string email)
    {
        var user = await authDal.GetUserByEmail(email);

        if (user.UserId != null)
        {
            throw new DuplicateEmailException();
        }
    }

    public async Task Register(UserModel user)
    {
        using (var scope = Helpers.CreatTransactionScope())
        {
            await dbSession.Lock();
            await ValidateEmail(user.Email);
            await CreatUser(user);
            scope.Complete();
        }
    }

    public async Task Login(int id)
    {
        await dbSession.SetUserId(id);
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await authDal.GetUserByEmail(email);

        if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
        {
            await Login(user.UserId ?? 0);
            return user.UserId ?? 0;
        }

        throw new AuthorizationException();
    }
}