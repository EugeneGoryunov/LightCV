﻿using Dapper;
using LightCV.DAL.Helpers;
using LightCV.DAL.Models;
using LightCV.DAL.Queries;
using Npgsql;

namespace LightCV.DAL;

public class DbSessionDAL : IDbSessionDAL
{
    public async Task<int> Create(SessionModel model)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            await connection.OpenAsync();
            return await connection.ExecuteAsync(QueriesContent.CreateSession, model);
        }
    }

    public async Task<SessionModel?> Get(Guid sessionId)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            await connection.OpenAsync();
            var sessions = 
                await connection.QueryAsync<SessionModel>(QueriesContent.GetSession, new {sessionId = sessionId});
            return sessions.FirstOrDefault();
        }
    }
    
    public async Task Lock(Guid sessionId)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            await connection.OpenAsync();
            string sql =
                @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";

            await connection.QueryAsync<SessionModel>(sql, new {sessionId = sessionId});

        }
    }

    public async Task<int> Update(SessionModel model)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            await connection.OpenAsync();
            return await connection.ExecuteAsync(QueriesContent.UpdateSession, model);
        }
    }
}