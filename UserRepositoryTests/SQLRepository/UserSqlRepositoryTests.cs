using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using UserRepository.Models;

namespace UserRepository.SQLRepository.Tests
{
    [TestClass()]
    public class UserSqlRepositoryTests
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly SqlConnection _connection;
        private readonly IConfiguration _configuration;
        public UserSqlRepositoryTests() 
        {

            _configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.test.json")
                           .Build();

            _connection = new SqlConnection(_configuration.GetConnectionString("UserDbConnection"));
            _userRepository = new UserSqlRepository(_connection, _configuration);
        }

        //Integration DB test
        [TestMethod()]
        public void User_DataBaseTest()
        {
            //Setup User
            User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = "Test";
            user.FullName = "TestFullName";
            user.Culture = "en-US";
            user.Email = "Test";
            user.Language = "en";
            user.Password = "P@ssword";

            try
            {
                //Create User
                var createdUser = _userRepository.CreateUser(user);
                //Get Inserted User
                var storedUser = _userRepository.GetUserById(createdUser.Data);

                Assert.IsNotNull(storedUser);
                Assert.AreEqual(createdUser.Message, "User Created");

                //Update User
                user.UserName = "New User Name";
                var updateUser = _userRepository.UpdateUser(user);

                //Get Updated User
                var updatedUser = _userRepository.GetUserById(storedUser.Data);

                Assert.IsNotNull(updatedUser);
                Assert.AreEqual(updatedUser.Data.UserName, "New User Name");
                Assert.IsNotNull(updatedUser.Data.DateUpdated);
                Assert.AreEqual(updatedUser.Data.DateUpdated.Value.Date, DateTime.UtcNow.Date);


                //Delete User
                _userRepository.DeleteUser(user);
                var deletedUser = _userRepository.GetUserById(user);
                Assert.AreEqual(deletedUser.Data.Id, Guid.Empty);

            }
            catch (Exception ex) 
            {
                //Test Failed
                Assert.IsNotNull(ex.Message);
            
            }
            finally
            {
                //Clear test data
                ClearTestData(user.Id);
            }
        }

        [TestMethod()]
        public void Test_Unique_Email()
        {
            //Setup User
            User user = new User();
            user.Id = Guid.NewGuid();
            user.UserName = "Test";
            user.FullName = "TestFullName";
            user.Culture = "en-US";
            user.Email = "TestUnique@email.com";
            user.Language = "en";
            user.Password = "P@ssword";

            try
            {
                //Create User
                var createdUser = _userRepository.CreateUser(user);
                //Get Inserted User
                var storedUser = _userRepository.GetUserById(createdUser.Data);

                Assert.IsNotNull(storedUser);
                Assert.AreEqual(createdUser.Message, "User Created");

                //Second User
                var secondUser = _userRepository.CreateUser(user);

                Assert.AreEqual(secondUser.Message, "User Allready Exists");

            }
            catch (Exception ex)
            {
                //Test Failed
                Assert.IsNotNull(ex.Message);
            }
            finally
            {
                //Clear test data
                ClearTestData(user.Id);
            }
        }

        private void ClearTestData(Guid id)
        {
            using(var command = _connection.CreateCommand()) 
            
            {
                command.CommandText = $"DELETE FROM [dbo].[Users] WHERE Id = '{id}'";
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
    }
}