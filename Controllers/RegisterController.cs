﻿using LightCV.BL.Auth;
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
            var errorModel = await authBl.ValidateEmail(model.Email ?? "");
            
            if (errorModel != null)
            {
                ModelState.TryAddModelError("Email", errorModel.ErrorMessage!);
            }

            if (ModelState.IsValid)
            {
                await authBl.CreatUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }

        }
        
        return View("Index", model);
    }
}