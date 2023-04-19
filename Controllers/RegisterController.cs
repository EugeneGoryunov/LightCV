using CurriculumVitae.BL.Auth;
using CurriculumVitae.ViewMappers;
using CurriculumVitae.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumVitae.Controllers;

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
    public IActionResult IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            authBl.CreatUser(AuthMapper.MapRegisterViewModelToUserModel(model));
            return Redirect("/");
        }
        
        return View("Index", model);
    }
}