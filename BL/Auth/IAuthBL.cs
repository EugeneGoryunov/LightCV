﻿using System.ComponentModel.DataAnnotations;

namespace CurriculumVitae.BL.Auth;

public interface IAuthBL
{
    Task<int> CreatUser(CurriculumVitae.DAL.Models.UserModel user);
    Task<ValidationResult> ValidateEmail(string email);
    Task<int> Authenticate(string email, string password, bool rememberMe);
}