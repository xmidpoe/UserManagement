using Common;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using UserRepository.Models;


namespace UserRepository.SQLRepository
{
    public class UserSqlRepository : IUserRepository<User>
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _configuration;
        public UserSqlRepository(IDbConnection connection, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = connection;

            if (string.IsNullOrEmpty(_db.ConnectionString))
                _db.ConnectionString = GetConnectionString();
        }

        public string GetConnectionString()
        {
            try
            {
                var value = _configuration.GetConnectionString("UserDbConnection");

                if (value == null)
                    throw new Exception("Missing Config Section");

                return value;

            }
            catch (Exception e)
            {
                throw new Exception("Exception looking for AppSettings. Inner Exception: " + e.Message);
            }
        }

        public RepoResponseMessage<User> CreateUser(User user)
        {
            var command = _db.CreateCommand();

            command.CommandText = "INSERT_NEW_USER";

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", user.Id));
            command.Parameters.Add(new SqlParameter("@USER_NAME", user.UserName));
            command.Parameters.Add(new SqlParameter("@FULL_NAME", user.FullName));
            command.Parameters.Add(new SqlParameter("@EMAIL", user.Email));
            command.Parameters.Add(new SqlParameter("@MOBILE_NUMBER", user.MobileNumber));
            command.Parameters.Add(new SqlParameter("@LANGUAGE", user.FullName));
            command.Parameters.Add(new SqlParameter("@CULTURE", user.Email));
            command.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));


            command.Connection.Open();


            using (command.Transaction = command.Connection.BeginTransaction())
            {
                try
                {
                    int result = command.ExecuteNonQuery();

                    if (result == -1)
                    {
                        command.Transaction.Commit();

                        return new RepoResponseMessage<User>
                        {
                            IsSuccess = true,
                            ErrorCode = 0,
                            Message = "User Created",
                            Data = user

                        };
                    }
                    else
                    {

                        command.Transaction.Rollback();

                        return new RepoResponseMessage<User>
                        {
                            IsSuccess = false,
                            ErrorCode = 1,
                            Message = "Error Creating User"
                        };
                    }
                }


                catch (SqlException ex)
                {
                    command.Transaction.Rollback();


                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = false,
                        ErrorCode = 1,
                        Message = ex.Number == 2627 ? "User Allready Exists" : ex.Message
                    };
                }
                finally { command.Connection.Close(); }
            }
        }

        public RepoResponseMessage<User> GetUserById(User user)
        {

            var command = _db.CreateCommand();

            command.CommandText = "GET_USER_BY_ID";

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", user.Id));

            command.Connection.Open();

            using (var reader = command.ExecuteReader())
            {
                try
                {
                    var userData = new User();

                    while (reader.Read())
                    {
                        userData.Id = reader.GetGuid(0);
                        userData.UserName = reader.GetString(1);
                        userData.FullName = reader.GetString(2);
                        userData.Email = reader.GetString(3);
                        userData.MobileNumber = (reader[4] == DBNull.Value) ? string.Empty : reader.GetString(4);
                        userData.Language = (reader[5] == DBNull.Value) ? string.Empty : reader.GetString(5);
                        userData.Culture = (reader[6] == DBNull.Value) ? string.Empty : reader.GetString(6);
                        userData.DateCreated = (reader[8] == DBNull.Value) ? null : reader.GetDateTime(8);
                        userData.DateUpdated = (reader[9] == DBNull.Value) ? null : reader.GetDateTime(9);

                    }

                    reader.Close();

                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = true,
                        ErrorCode = 0,
                        Message = "User Data Retrived",
                        Data = userData
                    };
                }

                catch (SqlException ex)
                {
                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = true,
                        ErrorCode = 0,
                        Message = ex.Message
                    };

                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public RepoResponseMessage<User> UpdateUser(User user)
        {
            var command = _db.CreateCommand();

            command.CommandText = "UPDATE_USER";

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", user.Id));
            command.Parameters.Add(new SqlParameter("@USER_NAME", user.UserName));
            command.Parameters.Add(new SqlParameter("@FULL_NAME", user.FullName));
            command.Parameters.Add(new SqlParameter("@EMAIL", user.Email));
            command.Parameters.Add(new SqlParameter("@MOBILE_NUMBER", user.MobileNumber));
            command.Parameters.Add(new SqlParameter("@LANGUAGE", user.FullName));
            command.Parameters.Add(new SqlParameter("@CULTURE", user.Email));
            command.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));

            command.Connection.Open();


            using (command.Transaction = command.Connection.BeginTransaction())
            {

                try
                {

                    int result = command.ExecuteNonQuery();

                    if (result == -1)
                    {
                        command.Transaction.Commit();

                        return new RepoResponseMessage<User>
                        {
                            IsSuccess = true,
                            ErrorCode = 0,
                            Message = "User Updated",
                            Data = user

                        };
                    }
                    else
                    {

                        command.Transaction.Rollback();

                        return new RepoResponseMessage<User>
                        {
                            IsSuccess = false,
                            ErrorCode = 1,
                            Message = "Error Updating User"
                        };
                    }
                }


                catch (SqlException ex)
                {
                    command.Transaction.Rollback();

                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = false,
                        ErrorCode = 1,
                        Message = ex.Message
                    };
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public RepoResponseMessage<User> DeleteUser(User user)
        {
            var command = _db.CreateCommand();

            command.CommandText = "DELETE_USER";

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", user.Id));
            command.Parameters.Add(new SqlParameter("@IS_DELETED", true));

            command.Connection.Open();


            using (command.Transaction = command.Connection.BeginTransaction())
            {

                try
                {
                    command.Connection.Open();


                    using (command.Transaction = command.Connection.BeginTransaction())
                    {
                        int result = command.ExecuteNonQuery();

                        if (result == -1)
                        {
                            command.Transaction.Commit();

                            return new RepoResponseMessage<User>
                            {
                                IsSuccess = true,
                                ErrorCode = 0,
                                Message = "User Deleted"
                            };
                        }
                        else
                        {

                            command.Transaction.Rollback();

                            return new RepoResponseMessage<User>
                            {
                                IsSuccess = false,
                                ErrorCode = 1,
                                Message = "Error Deleting User"
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    command.Transaction.Rollback();


                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = false,
                        ErrorCode = 1,
                        Message = ex.Message
                    };
                }
                finally { command.Connection.Close(); }
            }
        }

        public RepoResponseMessage<User> GetUserByUserNameAndPassword(User user)
        {
            var command = _db.CreateCommand();

            command.CommandText = "GET_USER_BY_USERNAME_AND_PASSWORD";

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@USER_NAME", user.UserName));
            command.Parameters.Add(new SqlParameter("@PASSWORD", user.Password));
            command.Connection.Open();

            using (var reader = command.ExecuteReader())
            {
                try
                {

                    var userData = new User();

                    while (reader.Read())
                    {
                        userData.Id = reader.GetGuid(0);
                        userData.UserName = reader.GetString(1);
                        userData.FullName = reader.GetString(2);
                        userData.Email = reader.GetString(3);
                        userData.MobileNumber = (reader[4] == DBNull.Value) ? string.Empty : reader.GetString(4);
                        userData.Language = (reader[5] == DBNull.Value) ? string.Empty : reader.GetString(5);
                        userData.Culture = (reader[6] == DBNull.Value) ? string.Empty : reader.GetString(6);
                        userData.DateCreated = (reader[8] == DBNull.Value) ? null : reader.GetDateTime(8);
                        userData.DateUpdated = (reader[9] == DBNull.Value) ? null : reader.GetDateTime(9);

                    }

                    reader.Close();

                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = true,
                        ErrorCode = 0,
                        Message = "User Data Retrived",
                        Data = userData
                    };
                }

                catch (SqlException ex)
                {
                    return new RepoResponseMessage<User>
                    {
                        IsSuccess = true,
                        ErrorCode = 0,
                        Message = ex.Message
                    };

                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
