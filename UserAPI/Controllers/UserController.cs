using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using UserAPI.Common;
using UserAPI.Models;
using UserManagement;
using UserRepository;
using UserRepository.Models;

namespace UserAPI.Controllers
{
    [Authorize()]
    public class UserController : Controller
    {
        private readonly IUserLogger _userLogger;
        private readonly IUserRepository<User> _userRepository;
        private IConfiguration _configuration;
        public UserController(IUserLogger userUogger, IUserRepository<User> userRepository, IConfiguration configuration)
        {;
            _userLogger = userUogger;
            _userRepository = userRepository;
            _configuration = configuration; 
        }

        [AllowAnonymous]
        [HttpPost("addUser")]
        public ActionResult<RepoResponseMessage<User>> AddUser([FromBody] User user)
        {
            RepoResponseMessage<User> responseMessage;

            var msg = new LogBuilder(this.HttpContext).GetMessage(MethodBase.GetCurrentMethod().Name, $"user: {user}");

            try
            {

                responseMessage = _userRepository.CreateUser(user);

                msg.Message = responseMessage.Message;

                _userLogger.LogDebugMessage(msg);

            }
            catch (Exception ex)
            {
                responseMessage = new RepoResponseMessage<User>() { ErrorCode = 1, IsSuccess = false,  Message = ex.Message };

                msg.Message = ex.Message;

                _userLogger.LogErrorMessage(msg);
            }

            _userLogger.LogInfoMessage(msg);

            return (responseMessage);
        }

        [HttpPut("updateUser")]
        public ActionResult<RepoResponseMessage<User>> UpdateUser([FromBody] User user)
        {
            RepoResponseMessage<User> responseMessage;

            var msg = new LogBuilder(this.HttpContext).GetMessage(MethodBase.GetCurrentMethod().Name, $"user: {user}");

            try
            {

                responseMessage = _userRepository.UpdateUser(user);

                msg.Message = responseMessage.Message;

                _userLogger.LogDebugMessage(msg);

            }
            catch (Exception ex)
            {
                responseMessage = new RepoResponseMessage<User>() { ErrorCode = 1, IsSuccess = false, Message = ex.Message };

                msg.Message = ex.Message;

                _userLogger.LogErrorMessage(msg);
            }

            _userLogger.LogInfoMessage(msg);

            return (responseMessage);
        }

        [HttpDelete("{id}")]
        public ActionResult<RepoResponseMessage<User>> DeleteUser(Guid id)
        {
            RepoResponseMessage<User> responseMessage;

            var msg = new LogBuilder(this.HttpContext).GetMessage(MethodBase.GetCurrentMethod().Name, $"Id: {id}"); 

            try
            {

                responseMessage = _userRepository.DeleteUser(new User { Id = id });

                msg.Message = responseMessage.Message;

            }
            catch (Exception ex)
            {
                responseMessage = new RepoResponseMessage<User>() { ErrorCode = 1, IsSuccess = false, Message = ex.Message };

                msg.Message = ex.Message;

                _userLogger.LogErrorMessage(msg);
            }

            _userLogger.LogInfoMessage(msg);

            return (responseMessage);
        }

        [HttpGet("getuser/{id}")]
        public ActionResult<RepoResponseMessage<User>> GetUser(Guid id)
        {
            RepoResponseMessage<User> responseMessage;

            var msg = new LogBuilder(this.HttpContext).GetMessage(MethodBase.GetCurrentMethod().Name, $"Id: {id}");

            try
            {
                responseMessage = _userRepository.GetUserById(new User { Id = id });

                msg.Message = responseMessage.Message;

                _userLogger.LogDebugMessage(msg);

            }
            catch (Exception ex) 
            {
                responseMessage = new RepoResponseMessage<User>() { ErrorCode = 1, IsSuccess = false, Message = ex.Message };

                msg.Message = ex.Message;

                _userLogger.LogErrorMessage(msg);
            }

            _userLogger.LogInfoMessage(msg);

            return (responseMessage);
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        public IActionResult LogIn([FromBody] UserLogin userLogin)
        {
            User user = new User();
            user.UserName = userLogin.UserName;
            user.Password = userLogin.Password;


            var msg = new LogBuilder(this.HttpContext).GetMessage(MethodBase.GetCurrentMethod().Name, $"UserName: {userLogin.UserName}");

            try
            {
                var responseMessage = _userRepository.GetUserByUserNameAndPassword(user);

                if (responseMessage.Data.Id != Guid.Empty)
                {
                    return Ok(GenerateToken(responseMessage.Data));
                }

                msg.Message = responseMessage.Message;

                _userLogger.LogDebugMessage(msg);
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;

                _userLogger.LogErrorMessage(msg);

            }

            return NotFound("User Not found. Please check your username and password!");
        }

        private string GenerateToken(User user)
        {

            var msg = new LogBuilder(this.HttpContext).GetMessage(MethodBase.GetCurrentMethod().Name, $"UserName: {user.UserName}");

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.FullName),
                new Claim(ClaimTypes.Email,user.Email)
            };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);


                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex) 
            {

                msg.Message = ex.Message;

                _userLogger.LogErrorMessage(msg);

                return string.Empty;
            }

        }
    }
}
