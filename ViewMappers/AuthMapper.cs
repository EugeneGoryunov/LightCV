﻿using CurriculumVitae.DAL.Models;
using CurriculumVitae.ViewModels;

namespace CurriculumVitae.ViewMappers;

public class AuthMapper
{
    public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
    {
        return new UserModel
        {
            Email = model.Email!,
            Password = model.Password!
        };
    }
}