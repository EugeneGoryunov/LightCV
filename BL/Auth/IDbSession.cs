﻿using LightCV.DAL.Models;

namespace LightCV.BL.Auth;

public interface IDbSession
{
    Task<SessionModel> GetSession();

    Task<int> SetUserId(int userId);

    Task<int?> GetUserId();

    Task<bool> IsLoggedIn();

    Task Lock();
}