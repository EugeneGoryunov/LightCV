﻿using System.ComponentModel.DataAnnotations;
using LightCV.DAL.Models;

namespace LightCV.BL.Auth;

public interface IAuthBL
{
    Task<int> CreatUser(UserModel user);
    Task<ValidationResult> ValidateEmail(string email);
    Task<int> Authenticate(string email, string password, bool rememberMe);
}