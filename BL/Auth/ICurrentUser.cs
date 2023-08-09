namespace LightCV.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();
}