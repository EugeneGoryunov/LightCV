using CurriculumVitae.DAL.Helpers;
using CurriculumVitae.DAL.Models;
using Dapper;
using Npgsql;

namespace CurriculumVitae.DAL;

public class AuthDal : IAuthDal
{
    public async Task<UserModel> GetUserByEmail(string email)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            connection.Open();
            
            return await connection.QueryFirstOrDefaultAsync<UserModel>
            (@"select UserId, Email, Password, Salt, Status
             from AppUser
             where Email = @email", 
                new { email = email }) ?? new UserModel();    
        }
    }

    public async Task<UserModel> GetUserById(string id)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<UserModel>
                (@"select UserId, Email, Password, Salt, Status
                 from AppUser 
                 where UserId = @id", 
                new { id = id }) ?? new UserModel();    
        }
    }

    public async Task<int> CreatUser(UserModel model)
    {
        using (var connection = new NpgsqlConnection(DBHelper.connectionString))
        {
            connection.Open();
            string sql = @"insert into AppUser(Email, Password, Salt, Status)
                         values(@Email, @Password, @Salt, @Status) returning UserId";
            return await  connection.QuerySingleAsync<int>(sql, model);
        }
    }
}