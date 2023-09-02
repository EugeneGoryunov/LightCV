using LightCV.BL.Auth;
using LightCV.BL.Exception;
using LightCV.ViewMappers;
using LightCV.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LightCV.Controllers;

public class RegisterController : Controller
{
    private readonly IAuthBL authBl;
    
    public RegisterController(IAuthBL authBl)
    {
        this.authBl = authBl;
    }

    [HttpGet]
    [Route("/register")]
    public IActionResult Index()
    {
        return View("Index", new RegisterViewModel());
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }
            catch (DuplicateEmailException e)
            {
                ModelState.TryAddModelError("Email", "Email уже существует");
            }


        }
        return View("Index", model);
    }
}