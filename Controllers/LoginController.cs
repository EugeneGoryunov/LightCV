using LightCV.BL.Auth;
using LightCV.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LightCV.Controllers;

public class LoginController : Controller
{
    private readonly IAuthBL authBl;

    public LoginController(IAuthBL authBl)
    {
        this.authBl = authBl;
    }

    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
    {
        return View("Index", new LoginViewModel());
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }
            catch (LightCV.BL.Exception.AuthorizationException)
            {
                ModelState.AddModelError("Email", "Логин или Email неверные");
            }
        }

        return View("Index", model);
    }
}